using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTableExporter.PlugPlay
{
    public static class ExportHelper
    {

        const string ColFormat = "\"{0}\",";
        public static void ExportToCSV<T>(this DbContext db, string FilePath, int MaxRows = int.MaxValue) where T : class
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
