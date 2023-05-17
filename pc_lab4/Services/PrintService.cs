using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class PrintService
    { 
        public static void ShowMenu()
        {
            Console.WriteLine("Client menu: ");
            Console.WriteLine("1. Send Data");
            Console.WriteLine("2. Start calculation");
            Console.WriteLine("3. Ask for result");
            Console.WriteLine("0. Exit");
        }

        public static void ShowConnectionMessage(int clientId)
        {
            Console.WriteLine($"Client #{clientId} connected.");
        }
    }
}
