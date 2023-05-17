namespace Services
{
    public class MatrixGeneratorService
    {   
        public static int[][] GenerateMatrix(int size)
        {
            int[][] matrix = new int[size][];

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new int[size];
            }

            Fill(matrix);

            return matrix;
        }
        public static IEnumerable<int> RandomIntsGenerator(int numberCount, int maxValue = 10, int minValue = 1)
        {
            var rnd = new Random();
            var list = new List<int>();

            for (int i = 0; i < numberCount; i++)
            {
                list.Add(rnd.Next(minValue, maxValue));
            }
            return list;
        }
        public static void Fill(int[][] matrix)
        {
            if (matrix == null)
            {
                Console.WriteLine("Matrix is null!");
                return;
            }
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i] == null)
                {
                    Console.WriteLine("Matrix row is null!");
                    return;
                }
                matrix[i] = RandomIntsGenerator(matrix.Length).ToArray();
            }
        }

    }
}
