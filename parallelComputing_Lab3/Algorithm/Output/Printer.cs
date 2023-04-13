using Data.LockTime;
using System.Diagnostics;

namespace Utils.Output
{
    public static class Printer
    {
        public static void PrintResult(int size, int min, int count, Stopwatch sw)
        {   
            Console.WriteLine("--- RESULTS ---");
            Console.WriteLine($" Size: {size}");
            Console.WriteLine($" Min: {min}");
            Console.WriteLine($" Count: {count}");
            Console.WriteLine($" Time: {((double)sw.ElapsedMilliseconds) / 1000} s");
        }

        public static void CurrentThreadInfo(int min, int count)
        {
            Console.WriteLine($"Min in {Environment.CurrentManagedThreadId} value: {min}");
            Console.WriteLine($"Count in {Environment.CurrentManagedThreadId} value: {count}");
        }

        public static void WaitTimeInfo()
        {
            Console.WriteLine("--- Total wait time ---");
            Console.WriteLine($" {(double)LockTime.GetTotalWaitTime() * 0.000001} ms");
        }
    }
}
