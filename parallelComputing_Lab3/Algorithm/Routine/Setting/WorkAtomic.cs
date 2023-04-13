using Data.Result;
using Utils.Routine.Interfaces;

namespace Utils.Routine.Setting
{
    public class WorkAtomic : IWorkable
    {
        public void SetMin(int newMin)
        {
            if (newMin < Results.min)
            {
                Interlocked.Exchange(ref Results.min, newMin);
            }
        }
        public void SetCount(int newCount)
        {
            Interlocked.Add(ref Results.count, newCount);
        }
    }
}
