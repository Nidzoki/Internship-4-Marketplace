using Marketplace.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Presentation
{
    public class Program
    {
        static void Main()
        {
            var marketplace = new Data.Program.Context();

            var UI = new UserInterface();
            while(UI.MainMenu(marketplace) != 0);
        }
    }
}
