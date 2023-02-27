
using System.Runtime.CompilerServices;
using System.Text;

namespace paralelComputing_Lab1.Services
{
    public class ExecutionResultService
    {
        public string filePath;

        public ExecutionResultService()
        {
            filePath = setRealtivePath("D:\\savings\\git\\Labs_6sem\\paralelComputing\\paralelComputing_Lab1\\paralelComputing_Lab1\\obj\\exData.txt");
        }

        public string setRealtivePath(string fullPath)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            return Path.GetRelativePath(currentDirectory, fullPath);
        } 
        public void WriteData(int matrixSize, long timeInMillisecondsElapsed, int threadCount = 1)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                string message = $"Matrix size: {matrixSize}, Time required for {threadCount} thread(s): {timeInMillisecondsElapsed}ms\n"; // replace with the data you want to write
                byte[] buffer = Encoding.UTF8.GetBytes(message);

                fileStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
