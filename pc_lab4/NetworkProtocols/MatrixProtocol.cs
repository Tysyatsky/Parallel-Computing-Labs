using Services;
using System.Net.Sockets;
using System.Text;

namespace NetworkProtocols
{
    public class MatrixProtocol
    {
        private readonly NetworkStream stream;
        private readonly MatrixConverter matrixConverter;

        public MatrixProtocol(NetworkStream stream)
        {
            this.stream = stream;
            this.matrixConverter = new MatrixConverter();
        }

        public void SendMatrix(int[][] matrix)
        {
            byte[] data = matrixConverter.ConvertMatrixToByteArray(matrix);
            SendData(data);
        }

        public int[][] ReceiveMatrix()
        {
            byte[] data = ReceiveData();
            return matrixConverter.ConvertByteArrayToMatrix(data);
        }

        public void SendCommand(string command)
        {
            byte[] data = Encoding.ASCII.GetBytes(command);
            SendData(data);
        }

        public string ReceiveCommand()
        {
            byte[] data = ReceiveData();
            return Encoding.ASCII.GetString(data);
        }

        public string AskForResult()
        {
            SendCommand("GET_RESULT");
            string response = ReceiveCommand();
            return response;
        }

        public void SendData(byte[] data)
        {
            // Send the byte array over the network stream
            stream.Write(data, 0, data.Length);
        }

        public byte[] ReceiveData()
        {
            byte[] buffer = new byte[1_024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead < buffer.Length)
            {
                // Trim the buffer to the actual message size
                Array.Resize(ref buffer, bytesRead);
            }

            return buffer;
        }

    }
}