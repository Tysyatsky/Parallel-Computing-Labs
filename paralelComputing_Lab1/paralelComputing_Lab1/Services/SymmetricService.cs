using paralelComputing_Lab1.Data;

namespace paralelComputing_Lab1.Services
{
    internal static class SymmetricService
    {
        public static void MakeSemetrical(Matrix matrix, int start, int size)
        {
            for (int i = start; i < size; i++)
            {
                for (int j = start; j < size - 1 - i; j++)
                {
                    matrix.SetNum(i, j, matrix.InnerMatrix[size - start - 1 - j][size - start - 1 - i]);
                }
            }
        }


        public static void MakeSemetricalAsync(Matrix matrix, int start, int size, int threadCount)
        {
            int rowsPerThread = size / threadCount; 

            Thread[] threads = new Thread[threadCount]; 

            for (int t = 0; t < threadCount; t++)
            {
                int threadIndex = t;

                threads[t] = new Thread(() =>
                {
                    int startRow = threadIndex * rowsPerThread;
                    int endRow = (threadIndex == threadCount - 1) ? size : (threadIndex + 1) * rowsPerThread;

                    for (int i = startRow; i < endRow; i++)
                    {
                        for (int j = 0; j < size - 1 - i; j++)
                        {
                            matrix.SetNum(i, j, matrix.InnerMatrix[size - start - 1 - j][size - start - 1 - i]);
                        }
                    }
                });

                threads[t].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }
    }
}
