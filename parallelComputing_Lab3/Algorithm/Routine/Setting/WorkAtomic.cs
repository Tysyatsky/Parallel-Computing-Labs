using Data.Result;
using Utils.Routine.Interfaces;

namespace Utils.Routine.Setting
{
    public class WorkAtomic : IWorkable
    {
        public void SetMin(int newMin)
        {
            InterlockedExchangeIfSmallerThan(ref Results.min, Results.min, newMin);
        }
        public void SetCount(int newCount)
        {
            Interlocked.Add(ref Results.count, newCount);
        }

        public static bool InterlockedExchangeIfSmallerThan(ref int location, int comparison, int newValue)
        {
            int initialValue;
            do
            {
                initialValue = location;
                if (initialValue > comparison) return false;
            }
            while (Interlocked.CompareExchange(ref location, newValue, initialValue) != initialValue);
            return true;
        }
    }
}
