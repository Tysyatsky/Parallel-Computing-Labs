using System.Net;
using System.Net.Sockets;
using NetworkProtocols;

namespace Server
{
    class CustomProtocolServer
    {
        static void Main()
        {
            // Listen for incoming connections on port 1234
            TcpListener listener = new(IPAddress.Any, 1234);
            listener.Start();
            Console.WriteLine("CustomProtocolServer started. Listening for connections on port 1234...");

            // Accept a client connection
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected.");

            // Create a CustomProtocol instance using the client's network stream
            CustomProtocol protocol = new(client.GetStream());

            // Wait for messages from the client and send them back
            while (true)
            {
                try
                {
                    byte[] message = protocol.Receive();
                    Console.WriteLine("Received message from client.");

                    // Modify the message before sending it back to the client
                    message[0] = (byte)(message[0] + 1);

                    protocol.Send(message);
                    Console.WriteLine("Sent message back to client.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    break;
                }
            }

            // Clean up the connection
            client.Close();
            listener.Stop();
            Console.WriteLine("CustomProtocolServer stopped.");
        }
    }
}