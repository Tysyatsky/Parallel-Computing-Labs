using System.Net.Sockets;
using System.Net;
using NetworkProtocols;

namespace Client
{
    internal class Program
    {   
        private static bool _resultState = false;
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Loopback, 1234);

            MatrixProtocol protocol = new MatrixProtocol(client.GetStream());

            int menuOption = int.MinValue;

            while (menuOption != 0)
            {
                ShowMenu();
                var readedOption = Console.ReadLine();

                if (int.TryParse(readedOption, out menuOption))
                {
                    switch (menuOption)
                    {   
                        case 1:
                            ProtocolAction(protocol);
                            break;
                        case 2:
                            ResultCheck(protocol);
                            break;
                        case 3:
                            break;
                        case 0:

                        default:
                            break;
                    }
                }
            }

            client.Close();
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
            for (int i = 0; i < 5; i++)
            {
                if (matrix[i] == null)
                {
                    Console.WriteLine("Matrix row is null!");
                    return;
                }
                matrix[i] = RandomIntsGenerator(5).ToArray();
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

        private static void ProtocolAction(MatrixProtocol protocol)
        {
            int[][] testMatrix = new int[5][];

            for (int i = 0; i < testMatrix.Length; i++)
            {
                testMatrix[i] = new int[5];
            }

            Fill(testMatrix);

            protocol.SendMatrix(testMatrix);

            var matrix = protocol.ReceiveMatrix();

            SwitchState();

            foreach (var array in matrix)
            {
                foreach (var item in array)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void ResultCheck(MatrixProtocol protocol)
        {
           // if()
        }
    }
}