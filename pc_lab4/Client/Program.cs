using System.Net.Sockets;
using System.Net;
using NetworkProtocols;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Loopback, 1234);

            // Create a CustomProtocol instance using the client's network stream
            MatrixProtocol protocol = new MatrixProtocol(client.GetStream());

            // Send 10 messages
            for (int i = 0; i < 10; i++)
            {
                byte[] message = new byte[1_024];
                message[0] = (byte)i; // Set the first byte to the message index
                protocol.SendData(message);
            }

            // Receive the 10 messages and check if they match the sent messages
            for (int i = 0; i < 10; i++)
            {
                byte[] receivedMessage = protocol.ReceiveData();
                Console.WriteLine($"Success: This message was received: {receivedMessage[0]}");
                if (receivedMessage[0] != i)
                {
                    // throw new Exception($"Received message {receivedMessage[0]} does not match expected message {i}.");
                    Console.WriteLine($"Error: This message was received: {receivedMessage[0]}");
                }
            }

            // Clean up the connection
            client.Close();
        }
    }
}