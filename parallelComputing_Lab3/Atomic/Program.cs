using Data.Initial;
using Data.Result;
using System.Diagnostics;
using System.Reflection;
using Utils.Output;
using Utils.Routine;
using Utils.Routine.Setting;

namespace Atomic
{
    internal class Program
    {
        static void Main()
        {   
            Work work = new(new WorkAtomic());
            foreach (var size in Constants.sizes)
            {
                List<int> ints = RandomizedData.GetRandomizedData(size);

                Stopwatch sw = Stopwatch.StartNew();

                Routine.Start(ints, work.DoWork);

                sw.Stop();

                Printer.PrintResult(size, Results.min, Results.count, sw);
                Results.Clear();
            }
        }
    }
}