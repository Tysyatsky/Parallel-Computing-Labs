namespace Services
{
    public class MatrixConverter
    {
        public int[][] ConvertByteArrayToMatrix(byte[] data)
        {
            int rows = 2;
            int columns = data.Length / (rows * sizeof(int));

            int[][] matrix = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new int[columns];
                Buffer.BlockCopy(data, i * columns * sizeof(int), matrix[i], 0, columns * sizeof(int));
            }

            return matrix;
        }

        public byte[] ConvertMatrixToByteArray(int[][] matrix)
        {
            int rows = matrix.Length;
            int columns = matrix[0].Length;

            byte[] data = new byte[rows * columns * sizeof(int)];

            Buffer.BlockCopy(matrix.SelectMany(row => row).ToArray(), 0, data, 0, data.Length);

            return data;
        }
    }
}