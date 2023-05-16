using Models;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace NetworkProtocols
{
    public class MatrixProtocol
    {
        private readonly NetworkStream stream;

        public MatrixProtocol(NetworkStream stream)
        {
            this.stream = stream;
        }

        public void SendData(ProtocolConfigurationData data)
        {
            var convertedData = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(data);
            stream.Write(convertedData, 0, convertedData.Length);
        }

        public ProtocolConfigurationData? ReceiveData()
        {
            byte[] buffer = new byte[1_024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead < buffer.Length)
            {
                Array.Resize(ref buffer, bytesRead);
            }
            var bytesAsString = Encoding.UTF8.GetString(buffer);
            return JsonConvert.DeserializeObject<ProtocolConfigurationData>(bytesAsString);
        }

        public void SendResultCommand(string command)
        {
            var convertedData = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(command);
            stream.Write(convertedData, 0, convertedData.Length);
        }

        public string ReceiveResultCommand()
        {
            byte[] buffer = new byte[1_024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead < buffer.Length)
            {
                Array.Resize(ref buffer, bytesRead);
            }
            return Encoding.UTF8.GetString(buffer);
        }

    }
}