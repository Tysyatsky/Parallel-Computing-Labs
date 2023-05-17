using System.Net;
using System.Net.Sockets;
using Models;
using NetworkProtocols;
using Services;

namespace Server
{
    class CustomProtocolServer
    {
        private static Socket? listener;
        static void Main()
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, 4000));
            listener.Listen(10);

            Console.WriteLine("CustomProtocolServer started. Listening for connections on port 4000...");

            while (true)
            {
                Socket client = listener.Accept();
                PrintService.ShowConnectionMessage(client.RemoteEndPoint);
                Task.Run(() => HandleClient(client));
            }
        }

        static void HandleClient(Socket client)
        {
            MatrixProtocol protocol = new(client);
            ProtocolConfigurationData? message = null;
            TaskCompletionSource<int[][]> source = new();
            Task<int[][]> task = source.Task;

            while (true)
            {
                try
                {   
                    if (message == null || message.Matrix == null)
                    {
                        message = protocol.ReceiveData();
                        protocol.SendCommand("Received data!");
                        continue;
                    }
                    var command = protocol.ReceiveCommand();

                    switch (command)
                    {
                        case "start":
                            Thread exec = new(async () =>
                            {
                                await SymmetricService.MakeSemetricalAsync(message.Matrix, message.Matrix.Length, message.ThreadCount, source);
                            });
                            exec.Start();
                            protocol.SendCommand("Calculation started!");
                            break;
                        case "result":
                            if (task.IsCompleted)
                            {
                                protocol.SendData(message);
                            }
                            else
                            {
                                protocol.SendCommand("Wait for the result to compute");
                            }
                            break;
                        case "finish":
                        default:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    return;
                }
            }
        }
    }
}