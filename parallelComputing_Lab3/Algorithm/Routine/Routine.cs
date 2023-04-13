using Data.Initial;

namespace Utils.Routine
{
    public class Routine
    {
        private static Thread[] threads = new Thread[Constants.threadCount];
        private static void StartRoutine(List<int> ints, Action<List<int>> Work)
        {
            int partSize = (int)System.Math.Ceiling((double)ints.Count() / Constants.threadCount);
            for (int i = 0; i < threads.Length; i++)
            {
                int startId = i * partSize;
                int endId = System.Math.Min(startId + partSize, ints.Count());

                int[] part = new int[endId - startId];
                ints.CopyTo(startId, part, 0, part.Length);
                List<int> listPart = part.ToList();

                threads[i] = new Thread(() =>
                {
                    Work(listPart);
                });

                threads[i].Start();
            }
        }

        private static void EndRoutine()
        {
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
        }

        public static void Start(List<int> ints, Action<List<int>> Work)
        {
            StartRoutine(ints, Work);
            EndRoutine();
        }
    }
}
