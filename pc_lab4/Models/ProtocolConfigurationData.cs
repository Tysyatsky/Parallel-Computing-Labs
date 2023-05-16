namespace Models
{
    [Serializable]
    public class ProtocolConfigurationData
    {
        public int ClientId;

        public int ThreadCount;

        public int[][]? Matrix;
        public ProtocolConfigurationData(int cliendId, int threadCount, int[][] matrix) 
        {
            ClientId = cliendId;
            ThreadCount = threadCount;
            Matrix = matrix;
        }
    }
}
