
using paralelComputing_Lab1.Data;
using paralelComputing_Lab1.Services;
using paralelComputing_Lab1.ExtenstionMethods;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace paralelComputing_Lab1
{
    internal class Program
    {
        private static Stopwatch _stopwatch = new Stopwatch();
        public static Matrix _matrix;

        static void Main(string[] args)
        {   
            while(true)
            {
                Console.Write("Enter matrix size: ");
                var mSizeS = Console.ReadLine();
                Console.Write("Enter threads count: ");
                var tCountS = Console.ReadLine();

                if (!int.TryParse(mSizeS, out int matrixSize) || !int.TryParse(tCountS, out int threadCount))
                {
                    Console.WriteLine("Error!");
                    return;
                }

                ExecutionResultService executionResultService = new ExecutionResultService();

                _stopwatch = new Stopwatch();
                _matrix = new Matrix(matrixSize);

                _stopwatch.Start();
                SymmetricService.MakeSemetrical(_matrix, 0, _matrix.InnerMatrix.Length);
                _stopwatch.Stop();
                //executionResultService.WriteData(matrixSize, _stopwatch.ElapsedMilliseconds);

                //_matrix.InnerMatrix.PrintMatrix(); //uncomment for debug
                //Console.WriteLine((float)_stopwatch.ElapsedMilliseconds);
                _matrix.Refill();

                _stopwatch = Stopwatch.StartNew();
                SymmetricService.MakeSemetricalAsync(_matrix, 0, _matrix.InnerMatrix.Length, threadCount);
                _stopwatch.Stop();

                executionResultService.WriteData(matrixSize, _stopwatch.ElapsedMilliseconds, threadCount);
                _matrix.InnerMatrix.PrintMatrix(); //uncomment for debug
                //Console.WriteLine((float)_stopwatch.ElapsedMilliseconds);
            }

        }
    }
}