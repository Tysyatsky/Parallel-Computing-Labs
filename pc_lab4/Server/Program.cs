using System.Net;
using System.Net.Sockets;
using Models;
using NetworkProtocols;
using Services;

namespace Server
{
    class CustomProtocolServer
    {
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
        }

        static void HandleClient(TcpClient client)
        {
            PrintService.ShowConnectionMessage(client.GetHashCode());
            MatrixProtocol protocol = new(client.GetStream());
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