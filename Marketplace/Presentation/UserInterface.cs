using Marketplace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Presentation
{
    public class UserInterface
    {
        public delegate int MyDelegate();

        public int MainMenu(Market marketplace)
        {
            Dictionary<string, MyDelegate> mainMenuOptionHandler = new Dictionary<string, MyDelegate>()
            {
                {"1", RegisterMenu },
                {"2", LogInMenu },
                {"3", Exit }
            };
            
            var askForOption = true;
            var option = string.Empty;

            while (askForOption)
            {
                Printer.PrintMainMenu();
                option = Console.ReadLine().Trim();
                
                if (!mainMenuOptionHandler.Keys.Contains(option))
                {
                    Console.Clear();
                    Console.WriteLine("\n ERROR! Input is not correct! Please try again.\n\n Press any key to continue...");
                    Console.ReadKey();
                    return -1;
                }
                askForOption = false;
            }
            return mainMenuOptionHandler[option].Invoke();
        }

        private int RegisterMenu()
        {
            Console.Clear();
            Console.WriteLine("\n REGISTER");
            Console.ReadKey();
            return 1;
        }

        private int LogInMenu()
        {   
            Console.Clear();
            Console.WriteLine("\n LOG IN");
            Console.ReadKey();
            return 1;
        }

        public int Exit() => 0;
    }
}
