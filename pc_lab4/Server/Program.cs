using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NetworkProtocols;

namespace Server
{
    class CustomProtocolServer
    {
        private static readonly ConcurrentDictionary<int, TcpClient> _clients = new();
        static void Main()
        {
            // Listen for incoming connections on port 1234
            TcpListener listener = new(IPAddress.Any, 1234);
            listener.Start();
            Console.WriteLine("CustomProtocolServer started. Listening for connections on port 1234...");

            while (_clients != null && _clients.Count < 10000)
            {
                // Accept a client connection
                TcpClient client = listener.AcceptTcpClient();
                if(_clients.TryAdd(client.GetHashCode(), client))
                {
                    Task.Run(() => HandleClient(client));
                }
            }
            listener.Stop();
        }

        static void HandleClient(TcpClient client)
        {
            Console.WriteLine("Client connected.");
            Thread.Sleep(7000);

            // Create a CustomProtocol instance using the client's network stream
            MatrixProtocol protocol = new(client.GetStream());

            // Wait for messages from the client and send them back
            try
            {
                // byte[] message = protocol.ReceiveData();
                int[][] message = protocol.ReceiveMatrix();
                Console.WriteLine("Received message from client.");

                // here needs to be future/promice bitchesssss

                protocol.SendMatrix(message);
                Console.WriteLine("Sent message back to client.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // break;
            }
            

            // Clean up the connection
            client.Close();
        }
    }
}