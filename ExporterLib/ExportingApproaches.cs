using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTableExporter.ExporterLib
{
    public static class ExportingApproaches
    {
        const string ColFormat = "\"{0}\",";
        public static void BaseLineExport<T>(this DbContext db, string FilePath, int MaxRows = int.MaxValue) where T : class
        {
            var properties = typeof(T).GetProperties();
            WriteHeader(typeof(T), FilePath);
            var Table = db.Set<T>().AsNoTracking();
            if (MaxRows != int.MaxValue)
            {
                Table = Table.Take(MaxRows);
            }
            var rows = Table.ToList();
            using (StreamWriter streamWriter = new StreamWriter(FilePath, true))
            {
                while (rows.Any())
                {
                    foreach (var colName in properties)
                    {
                        var ColData = typeof(T).GetProperty(colName.Name)?.GetValue(rows[0]);
                        streamWriter.Write(string.Format(ColFormat, ColData));
                    }
                    streamWriter.WriteLine();
                    streamWriter.Flush();
                    rows.RemoveAt(0);
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
            //}
        }

        public static void BatchingExport<T>(this DbContext db, string FilePath, int MaxRows = int.MaxValue) where T : class
        {
            var RowsRead = 0;
            WriteHeader(typeof(T), FilePath);
            var properties = typeof(T).GetProperties();
            var Total = 0;
            if (MaxRows == int.MaxValue)
            {
                Total = db.Set<T>().AsNoTracking().Count();
            }
            else
            {
                Total = MaxRows;
            }
            var BatchSize = (int)Math.Round(0.1 * Total);
        BatchStart:
            var CurrBatch = (MaxRows - RowsRead) >= BatchSize ? BatchSize : (MaxRows - RowsRead);
            if (CurrBatch == 0)
            {
                return;
            }
            var Table = db.Set<T>().AsNoTracking();
            Table = Table.Skip(RowsRead).Take(CurrBatch);
            var rows = Table.ToList();
            var CurrentCount = rows.Count();
            if (CurrentCount == 0)
            {
                return;
            }
            RowsRead += CurrentCount;
            using (StreamWriter streamWriter = new StreamWriter(FilePath, true))
            {
                while (rows.Any())
                {
                    foreach (var colName in properties)
                    {
                        var ColData = typeof(T).GetProperty(colName.Name)?.GetValue(rows[0]);
                        streamWriter.Write(string.Format(ColFormat, ColData));
                    }
                    rows.RemoveAt(0);
                    streamWriter.WriteLine();
                    streamWriter.Flush();
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
            goto BatchStart;
        }

        public static void ForEachExport<T>(this DbContext db, string FilePath, int MaxRows = int.MaxValue) where T : class
        {
            WriteHeader(typeof(T), FilePath);
            var properties = typeof(T).GetProperties();
            var Table = db.Set<T>().AsNoTracking();
            if (MaxRows != int.MaxValue)
            {
                Table = Table.Take(MaxRows);
            }


            using (StreamWriter streamWriter = new StreamWriter(FilePath, true))
            {
                foreach (var row in Table)
                {
                    foreach (var colName in properties)
                    {
                        var ColData = typeof(T).GetProperty(colName.Name)?.GetValue(row);
                        streamWriter.Write(string.Format(ColFormat, ColData));
                    }
                    streamWriter.WriteLine();
                    streamWriter.Flush();
                }
                streamWriter.Flush();
                streamWriter.Close();
            }

        }

        public static void CreateCommandExport<T>(this DbContext db, string FilePath, int MaxRows = int.MaxValue) where T : class
        {
            WriteHeader(typeof(T), FilePath);
            var Table = db.Set<T>().AsNoTracking();
            if (MaxRows != int.MaxValue)
            {
                Table = Table.Take(MaxRows);
            }
            using (var command = db.Database.GetDbConnection().CreateCommand())
            {
                bool wasOpen = command.Connection?.State == ConnectionState.Open;
                if (!wasOpen) command.Connection?.Open();
                try
                {
                    command.CommandText = Table.ToQueryString();
                    using (StreamWriter streamWriter = new StreamWriter(FilePath, true))
                    {
                        using (var result = command.ExecuteReader())
                        {
                            while (result.Read())
                            {
                                for (int col = 0; col < result.FieldCount; col++)
                                {
                                    streamWriter.Write(string.Format(ColFormat, result[col].ToString()));
                                }
                                streamWriter.WriteLine();
                                streamWriter.Flush();
                            }
                        }
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                finally
                {
                    if (!wasOpen) command.Connection?.Close();
                }
            }

        }

        private static void WriteHeader(Type table, string FilePath)
        {
            using (FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var colName in table.GetProperties())
                    {
                        streamWriter.Write(string.Format(ColFormat, colName.Name));
                    }
                    streamWriter.WriteLine();
                    streamWriter.Flush();
                    streamWriter.Close();
                }

            }
        }
    }
}
