using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Utils.Math;
using Data.Initial;
using Utils.Output;
using Utils.Routine;
using Data.Result;
using System.Reflection;
using Utils.Routine.Setting;
using Data.LockTime;

namespace Lock
{
    internal class Program
    {
        static void Main()
        {
            Work work = new(new WorkLock());
            foreach (var size in Constants.sizes)
            {
                List<int> ints = RandomizedData.GetRandomizedData(size);

                Stopwatch sw = Stopwatch.StartNew();

                Routine.Start(ints, work.DoWork);

                sw.Stop();

                Printer.PrintResult(size, Results.min, Results.count, sw);
                Printer.WaitTimeInfo();
                Results.Clear();
            }
        }
    }
}