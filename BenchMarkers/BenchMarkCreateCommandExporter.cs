using EFTableExporter.ExporterLib;

namespace EFTableExporter.BenchMarkers
{
    public class BenchMarkCreateCommandExporter : BenchMarkBase
    {
        public new void RunExport()
        {
            db.CreateCommandExport<DataTable1>(FileName, Rows);
        }
         
    }
}
