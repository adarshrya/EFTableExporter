using EFTableExporter.ExporterLib;

namespace EFTableExporter.BenchMarkers
{
    public class BenchMarkBatchingExporter : BenchMarkBase
    {
        public new void RunExport()
        {
            db.BatchingExport<DataTable1>(FileName, Rows);
        }
    }
}
