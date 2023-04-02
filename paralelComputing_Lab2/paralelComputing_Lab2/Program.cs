using Data.Interfaces;
using Data.Models;
using Instances;
using System.Threading;

namespace paralelComputing_Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Instances.ThreadPool threadPool = new Instances.ThreadPool(4);
            Thread init = new Thread(() =>
            {
                while (true)
                {   
                    if(threadPool.IsDisposed)
                    {
                        return;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        var task = Generators.Instance.RandomTaskGenerator.GetTask();
                        threadPool.AddTask(task);
                        Thread.Sleep(task.ExecutionTime * 200);
                    }
                }
            });
            init.Start();
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(40000);
                threadPool.StartIteration();
            }
            // init.Join();
            threadPool.Dispose();
        }

        static void QueueTest(CustomQueue strings)
        {
            FakeTask fakeTask = Generators.Instance.RandomTaskGenerator.GetTask();
            Console.WriteLine($"Waiting {fakeTask.Id}");
            strings.Push(fakeTask);
            Console.WriteLine(strings.Size());
            strings.Pop(); 
            Console.WriteLine($"WORK DONE {fakeTask.Id}");
        }
    }
}