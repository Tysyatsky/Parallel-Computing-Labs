using System.Net.Sockets;
using System.Net;
using NetworkProtocols;
using Models;
using Services;

namespace Client
{
    internal class Program
    {   
        private static string serverIP = "127.0.0.1";
        private static int serverPort = 4000;
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            socket.Connect(serverIP, serverPort);

            MatrixProtocol protocol = new(socket);

            while (true)
            {
                PrintService.ShowMenu();
                var readedOption = Console.ReadLine();

                if (!int.TryParse(readedOption, out int menuOption))
                {
                    Console.WriteLine("Not valid menu option");
                    continue;
                }

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
                        SendConfig(configuration, protocol);
                        continue;
                    case 2:
                        StartCalculation(protocol);
                        continue;
                    case 3:
                        ReceiveResult(protocol);
                        continue;
                    case 0:
                    default:
                        Shutdown(protocol);
                        return;
                }
            }
        }

        private static void SendConfig(ProtocolConfigurationData configuration, MatrixProtocol protocol)
        {
            protocol.SendData(configuration);
            Console.WriteLine("Sended config");
            Console.WriteLine(protocol.ReceiveCommand());
        }

        private static void StartCalculation(MatrixProtocol protocol)
        {
            protocol.SendCommand("start");
            Console.WriteLine("Sended command start");
            Console.WriteLine(protocol.ReceiveCommand());
        }

        private static void ReceiveResult(MatrixProtocol protocol)
        {
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
        }

        private static void Shutdown(MatrixProtocol protocol)
        {
            protocol.SendCommand("finish");
            socket.Shutdown(SocketShutdown.Both);
        }
    }
}