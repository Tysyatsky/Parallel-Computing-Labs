using System.Net.Sockets;
using System.Net;
using NetworkProtocols;
using Models;
using Services;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new();
            client.Connect(IPAddress.Loopback, 4000);

            MatrixProtocol protocol = new(client.GetStream());

            while (true)
            {
                PrintService.ShowMenu();
                var readedOption = Console.ReadLine();

                if (int.TryParse(readedOption, out int menuOption))
                {
                    if (!int.TryParse(args[0], out int size)
                    || !int.TryParse(args[1], out int threads))
                    {
                        return;
                    }

                    var matrix = MatrixGeneratorService.GenerateMatrix(size);

                    var configuration = new ProtocolConfigurationData(threads, matrix);

                    switch (menuOption)
                    {
                        case 1:
                            protocol.SendData(configuration);
                            Console.WriteLine("Sended config");
                            Console.WriteLine(protocol.ReceiveCommand());
                            continue;
                        case 2:
                            protocol.SendCommand("start");
                            Console.WriteLine("Sended command start");
                            Console.WriteLine(protocol.ReceiveCommand());
                            continue;
                        case 3:
                            protocol.SendCommand("result");
                            ProtocolConfigurationData? data = protocol.ReceiveData();
                            if (data != null && data.Matrix != null)
                            {
                                foreach (var array in data.Matrix)
                                {
                                    foreach (var item in array)
                                    {
                                        Console.Write(item + " ");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            continue;
                        case 0:
                        default:
                            protocol.SendCommand("finish");
                            client.Close();
                            return;
                    }
                }
            }
        }
    }
}