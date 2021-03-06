using BenchmarkDotNet.Running;
using EFTableExporter.BenchMarkers;

namespace EFTableExporter
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var result = BenchmarkSwitcher.FromTypes(new[] {
                typeof(BenchMarkBaseLineExporter),
                typeof(BenchMarkForEachExporter),
                typeof(BenchMarkCreateCommandExporter),
                typeof(BenchMarkBatchingExporter)
            }).Run(args);
        }
    }
}
