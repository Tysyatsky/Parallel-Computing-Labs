using System.Collections.Concurrent;

namespace Data.LockTime
{
    public static class LockTime
    {
        static readonly ConcurrentQueue<long> _measured = new ConcurrentQueue<long>();

        public static void Add(long time)
        {
            _measured.Enqueue(time);
        }

        private static void Clear() { _measured.Clear();}

        public static long GetTotalWaitTime() 
        {
            long sum = _measured.Sum();
            Clear();
            return sum;
        }
    }
}
