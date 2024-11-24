using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Presentation
{
    public static class Printer
    {
        public static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n WELCOME TO MARKET PLACE APP\n\n Main menu:\n\n 1. Register\n 2. Log in\n 3. Exit");
            Console.Write("\n Your input: ");
        }
    }
}
