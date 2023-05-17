using Models;
using System.Drawing;

namespace Services
{
    public static class SymmetricService
    {
        public static async Task MakeSemetricalAsync(int[][] matrix, int size, int threadCount, TaskCompletionSource<int[][]> result)
        {
            int rowsPerThread = size / threadCount; 

            Thread[] threads = new Thread[threadCount]; 

            for (int t = 0; t < threadCount; t++)
            {
                int threadIndex = t;

                threads[t] = new Thread(() => Iteration(matrix, threadIndex, rowsPerThread, threadCount, size));

                threads[t].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            await Task.Delay(10000);

            if (result.TrySetResult(matrix)) 
            {
                Console.WriteLine("Result was set!");
            }

        }

        private static void Iteration(int[][] matrix, int threadIndex, int rowsPerThread, int threadCount, int size)
        {
            int startRow = threadIndex * rowsPerThread;
            int endRow = (threadIndex == threadCount - 1) ? size : (threadIndex + 1) * rowsPerThread;

            for (int i = startRow; i < endRow; i++)
            {
                for (int j = 0; j < size - 1 - i; j++)
                {
                    matrix[i][j] = matrix[size - 1 - j][size - 1 - i];
                }
            }
        }
    }
}
