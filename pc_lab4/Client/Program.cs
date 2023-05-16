using System.Net.Sockets;
using System.Net;
using NetworkProtocols;
using System.Net.Http.Headers;
using Models;

namespace Client
{
    internal class Program
    {   
        private static bool _resultState = false;
        static void Main(string[] args)
        {

            TcpClient client = new();
            client.Connect(IPAddress.Loopback, 4000);

            MatrixProtocol protocol = new(client.GetStream());

            while (true)
            {
                ShowMenu();
                var readedOption = Console.ReadLine();

                if (int.TryParse(readedOption, out int menuOption))
                {
                    switch (menuOption)
                    {
                        case 1:
                            ProtocolActionSend(protocol, args[0], args[1], args[2]);
                            break;
                        case 2:
                            // ResultCheck(protocol);
                            break;
                        case 3:
                            ProtocolActionRecieve(protocol);
                            break;
                        case 0:
                            client.Close();
                            return;
                        default:
                            break;
                    }
                }
            }
        }

        private static void SwitchState()
        {
            _resultState = !_resultState;
        }

        private static IEnumerable<int> RandomIntsGenerator(int numberCount, int maxValue = 10, int minValue = 1)
        {
            var rnd = new Random();
            var list = new List<int>();

            for (int i = 0; i < numberCount; i++)
            {
                list.Add(rnd.Next(minValue, maxValue));
            }
            return list;
        }
        private static void Fill(int[][] matrix)
        {   
            if(matrix == null)
            {
                Console.WriteLine("Matrix is null!");
                return;
            }
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i] == null)
                {
                    Console.WriteLine("Matrix row is null!");
                    return;
                }
                matrix[i] = RandomIntsGenerator(matrix.Length).ToArray();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Client menu: ");
            Console.WriteLine("1. Send Data");
            Console.WriteLine("2. Start calculation");
            Console.WriteLine("3. Ask for result");
            Console.WriteLine("0. Exit");
        }

        private static void ProtocolActionSend(MatrixProtocol protocol, string clientId, string threadCount, string paramSize)
        {   
            if(!int.TryParse(paramSize, out int size) 
                || !int.TryParse(clientId, out int client) 
                || !int.TryParse(threadCount, out int threads))
            {
                return;
            }

            int[][] testMatrix = new int[size][];

            for (int i = 0; i < testMatrix.Length; i++)
            {
                testMatrix[i] = new int[size];
            }

            Fill(testMatrix);

            var configuration = new ProtocolConfigurationData(client, threads, testMatrix);

            protocol.SendData(configuration);
        }

        private static void ProtocolActionRecieve(MatrixProtocol protocol)
        {
            var data = protocol.ReceiveData();

            SwitchState();
            
            if(data != null && data.Matrix != null)
            {
                foreach (var array in data.Matrix)
                {
                    foreach (var item in array)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
        }

        //private static void StartCalculation(MatrixProtocol protocol)
        //{
        //    protocol.SendData();
        //}
    }
}