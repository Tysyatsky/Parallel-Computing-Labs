namespace Models
{
    [Serializable]
    public class ProtocolConfigurationData
    {
        public int ThreadCount;

        public int[][]? Matrix;
        public ProtocolConfigurationData(int threadCount, int[][] matrix) 
        {
            ThreadCount = threadCount;
            Matrix = matrix;
        }
    }
}
