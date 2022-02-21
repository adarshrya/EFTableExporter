using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Diagnosers;

namespace EFTableExporter.BenchMarkers
{

    [MinColumn, MaxColumn, RankColumn, IterationsColumn, Q1Column, Q3Column]
    [EventPipeProfiler(EventPipeProfile.GcVerbose)]
    [MaxIterationCount(5)]
    [MinIterationCount(4)]  
    public class BenchMarkBase
    {
        public MasterDbContext db;

        [GlobalSetup]
        public void DbInit()
        {
            db = new MasterDbContext();
        }

        [Params(500000,1000000,1500000,2000000)]
        public int Rows { get; set; }
                
        public static string FileName = @"Out.csv";

        [Benchmark]
        public void RunExport() => throw new NotImplementedException();

        [GlobalCleanup]
        public void DbClean()
        {
            db.Dispose();
            db = null;
        }

        [IterationCleanup]
        public void IterationCleanup()
          => File.Delete(FileName);
    }

}
