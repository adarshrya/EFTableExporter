using EFTableExporter.ExporterLib;

namespace EFTableExporter.BenchMarkers
{
    public class BenchMarkBaseLineExporter : BenchMarkBase
    {
        public new void RunExport()
        {
            db.BaseLineExport<DataTable1>(FileName, Rows);
        }
    }
}
