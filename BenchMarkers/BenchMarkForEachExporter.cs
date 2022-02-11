using EFTableExporter.ExporterLib;

namespace EFTableExporter.BenchMarkers
{
    public class BenchMarkForEachExporter : BenchMarkBase
    {
        public new void RunExport()
        {
            db.ForEachExport<DataTable1>(FileName, Rows);
        } 
    }
}
