using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTableExporter.BenchMarkers
{
    public class TimePrint : IDisposable
    {
        private string msg = "";
        private DateTime time = DateTime.Now;
        public TimePrint(string msg)
        {
            this.msg = msg;
            var ramUsage = Process.GetCurrentProcess().WorkingSet64;
            var allocationInMB = ramUsage / (1024 * 1024);
            Console.WriteLine($"{msg} {allocationInMB} MB");
        }
        public void Dispose()
        {
            var _time = DateTime.Now - time;
            Console.WriteLine($"{msg} {_time.TotalSeconds}");
            var ramUsage = Process.GetCurrentProcess().WorkingSet64;
            var allocationInMB = ramUsage / (1024 * 1024);
            Console.WriteLine($"{msg} {allocationInMB} MB");
            Console.WriteLine($"{msg} {allocationInMB/ _time.TotalSeconds} MB/s");
        }
    }
}
