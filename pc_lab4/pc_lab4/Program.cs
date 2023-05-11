using Models;
using Services;
using System.Text;

namespace pc_lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[][] matrix = new int[5][];

            for (int i = 0; i < 5; i++)
            {
                matrix[i] = new int[5];
            }

            FillMatrix(matrix, 0, 10);

            foreach (var array in matrix)
            {
                foreach (var item in array)
                {
                    Console.WriteLine(item.ToString());
                }
            }

            MatrixConverter matrixConverter = new();
            var byteArray = matrixConverter.ConvertMatrixToByteArray(matrix);
            Console.WriteLine(Encoding.UTF8.GetString(byteArray));
            var matrixT = matrixConverter.ConvertByteArrayToMatrix(byteArray);
            foreach (var array in matrixT)
            {
                foreach (var item in array)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        public static void FillMatrix(int[][] matrix, int startValue, int increment)
        {
            int rows = matrix.Length;
            int columns = matrix[0].Length;
            int value = startValue;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i][j] = value;
                    value += increment;
                }
            }
        }
    }
}