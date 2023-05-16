using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Models;
using NetworkProtocols;

namespace Server
{
    public enum Commands
    {
        START_CALCULATION,
        GET_RESULT,
    }
    class CustomProtocolServer
    {
        private static ConcurrentBag<ProtocolConfigurationData> _clientsData = new();
        static void Main()
        {
            TcpListener listener = new(IPAddress.Any, 4000);
            listener.Start();
            Console.WriteLine("CustomProtocolServer started. Listening for connections on port 4000...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
            // listener.Stop();
        }

        static void HandleClient(TcpClient client)
        {
            Console.WriteLine("Client connected.");
            Thread.Sleep(7000);

            MatrixProtocol protocol = new(client.GetStream());

            // bool IsStopped = false;

            while (true)
            {
                try
                {  
                    ProtocolConfigurationData message = protocol.ReceiveData() ?? new ProtocolConfigurationData(-1, 1, new int[1][]);

                    if(message != null 
                        && message.ClientId != -1 
                        && message.Matrix != null) 
                    {
                        Console.WriteLine("Received message from client.");

                        //if (protocol.ReceiveResultCommand() != Commands.START_CALCULATION.ToString())
                        //{
                        //    continue;
                        //}

                        TaskCompletionSource<int[][]> source = new();

                        Task<int[][]> task = source.Task;

                        Thread exec = new Thread(() =>
                        {
                            Services.SymmetricService.MakeSemetricalAsync(message.Matrix, 0, message.Matrix.Length, message.ThreadCount, source);
                        });

                        exec.Start();

                        if(Task.CompletedTask.IsCompleted)
                        {
                            protocol.SendData(message);
                        }
                        else
                        {
                            protocol.SendResultCommand("Wait for the result to compute");
                        }
                        Console.WriteLine("Sent message back to client.");
                    }
                    else
                    {
                        Console.WriteLine("Client was not connected. Try again later.");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            client.Close();
        }
    }
}