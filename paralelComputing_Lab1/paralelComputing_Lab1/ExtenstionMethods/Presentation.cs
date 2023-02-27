using System.Collections;

namespace paralelComputing_Lab1.ExtenstionMethods
{
    internal static class Presentation
    {
        public static void PrintMatrix<T>(this T ints) 
            where T : IEnumerable<IEnumerable>
        {
            foreach (var row in ints)
            {
                foreach (var number in row)
                {
                    Console.Write(number + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
