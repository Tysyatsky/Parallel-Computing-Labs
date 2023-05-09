using System.Net.Sockets;

namespace NetworkProtocols
{
    public class CustomProtocol
    {
        private readonly NetworkStream stream;

        public CustomProtocol(NetworkStream stream)
        {
            this.stream = stream;
        }

        public void Send(byte[] message)
        {
            stream.Write(message, 0, message.Length);
        }

        public byte[] Receive()
        {
            byte[] buffer = new byte[100];
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