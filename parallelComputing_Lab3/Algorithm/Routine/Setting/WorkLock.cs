using Data.Initial;
using Data.LockTime;
using Data.Result;
using System.Diagnostics;
using Utils.Routine.Interfaces;

namespace Utils.Routine.Setting
{
    public class WorkLock : IWorkable
    {
        private static object @lock = new();

        private Stopwatch[] _stopWatch = new Stopwatch[Constants.threadCount];
        public static object Lock { get => @lock; set => @lock = value; }

        public WorkLock() 
        {
            for (int i = 0; i < Constants.threadCount; i++)
            {
                _stopWatch[i] = new Stopwatch();
            }
        }

        private void RecordTime(int timerId)
        {
            _stopWatch[timerId].Stop();
            long elapsedTicks = _stopWatch[timerId].ElapsedTicks;
            long elapsedNanoSeconds = elapsedTicks * 1000000000L / Stopwatch.Frequency;
            LockTime.Add(elapsedNanoSeconds);
            _stopWatch[timerId].Reset();
        }

        private int SetNewStopWatch()
        {
            for (int i = 0; i < Constants.threadCount; i++)
            {
                if (!_stopWatch[i].IsRunning)
                {
                    _stopWatch[i].Start();
                    return i;
                }
            }
            return -1;
        }
        public void SetMin(int newMin)
        {
            int timerId = SetNewStopWatch();
            lock (Lock)
            {
                if (newMin < Results.min)
                {
                    Results.min = newMin;
                }
            }
            RecordTime(timerId);
        }
        public void SetCount(int newCount)
        {
            int timerId = SetNewStopWatch();
            lock (Lock)
            {
                Results.count += newCount;
            }
            RecordTime(timerId);
        }
    }
}
