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

            int[][] testMatrix = new int[5][];

            for (int i = 0; i < testMatrix.Length; i++)
            {
                testMatrix[i] = new int[5];
            }

            //byte[] message = new byte[1_024];
            //for (int i = 0; i < testMatrix.LongLength; i++)
            //{
            //    for (int j = 0; j < testMatrix[i].Length; j++)
            //    {
            //        message[i] = (byte)testMatrix[i][j]; // Set the first byte to the message index
            //        protocol.SendData(message);
            //        i++;
            //    }
            //}

            protocol.SendMatrix(testMatrix);

            // Receive the 10 messages and check if they match the sent messages
            //for (int i = 0; i < 10; i++)
            //{
            //    byte[] receivedMessage = protocol.ReceiveData();
            //    Console.WriteLine($"Success: This message was received: {receivedMessage[0]}");
            //    if (receivedMessage[0] != i)
            //    {
            //        // throw new Exception($"Received message {receivedMessage[0]} does not match expected message {i}.");
            //        Console.WriteLine($"Error: This message was received: {receivedMessage[0]}");
            //    }
            //}

            var matrix = protocol.ReceiveMatrix();

            foreach (var item in matrix)
            {

            }

            Console.WriteLine(matrix);

            // Clean up the connection
            client.Close();
        }

        private IEnumerable<int> RandomIntsGenerator(int numberCount, int maxValue = 10, int minValue = 1)
        {
            var rnd = new Random();
            var list = new List<int>();

            for (int i = 0; i < numberCount; i++)
            {
                list.Add(rnd.Next(minValue, maxValue));
            }
            return list;
        }
        private void Fill(int[][] matrix)
        {   
            if(matrix != null)
            {   
                for (int i = 0; i < 5; i++)
                {
                    if (matrix[i] != null)
                    {
                        matrix[i] = RandomIntsGenerator(5).ToArray();
                    }
                }
            }
        }
    }
}