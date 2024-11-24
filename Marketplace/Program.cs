using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Domain.Entities;
using Marketplace.Presentation;

namespace Marketplace
{
    internal class Program
    {
        static void Main()
        {
            var marketplace = new Market();
            var UI = new UserInterface();

            var quit = false;
            while (!quit)
            {
                if (UI.MainMenu(marketplace) == 0)
                    quit = true;
            }
        }
    }
}
