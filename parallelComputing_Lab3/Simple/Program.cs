using System.Diagnostics;
using Utils.Math;
using Data.Initial;
using Utils.Output;

namespace Simple
{
    internal class Program
    {   
        static void Main(string[] args)
        {
            foreach (var size in Constants.sizes)
            {
                var list = RandomizedData.GetRandomizedData(size);
                int mod = Constants.mod;

                Stopwatch sw = Stopwatch.StartNew();

                int elemCount = list.ListCheck(mod, Functions.isDevided);

                int min = list.MinWithCheck(mod, Functions.isDevided);

                sw.Stop();

                Printer.PrintResult(size, min, elemCount, sw);
            }
        }
    }
}