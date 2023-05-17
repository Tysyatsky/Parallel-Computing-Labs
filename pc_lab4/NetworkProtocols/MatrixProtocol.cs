using Models;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace NetworkProtocols
{
    public class MatrixProtocol
    {
        private readonly Socket socket;

        public MatrixProtocol(Socket socket)
        {
            this.socket = socket;
        }

        public void SendData(ProtocolConfigurationData data)
        {
            var convertedData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            socket.Send(convertedData);
        }

        public ProtocolConfigurationData? ReceiveData()
        {
            byte[] buffer = new byte[100_240];
            int bytesRead = socket.Receive(buffer);
            if (bytesRead < buffer.Length)
            {
                Array.Resize(ref buffer, bytesRead);
            }
            var bytesAsString = Encoding.UTF8.GetString(buffer);
            try
            {
                var convertedData = JsonConvert.DeserializeObject<ProtocolConfigurationData>(bytesAsString);
                return convertedData;
            }
            catch (JsonSerializationException)
            {
                Console.WriteLine(Encoding.UTF8.GetString(buffer).Trim(new char[] { '"' }));
                return null;
            }
        }

        public void SendCommand(string command)
        {   
            var convertedData = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(command);
            socket.Send(convertedData);
        }

        public string ReceiveCommand()
        {
            byte[] buffer = new byte[1_240];
            int bytesRead = socket.Receive(buffer);

            if (bytesRead < buffer.Length)
            {
                Array.Resize(ref buffer, bytesRead);
            }
            return Encoding.UTF8.GetString(buffer).Trim(new char[] { '"' });
        }
    }
}