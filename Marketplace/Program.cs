using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Data.Entities;
using Marketplace.Data.Enums;
using Marketplace.Presentation;

namespace Marketplace
{
    internal class Program
    {
        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var marketplace = new Market();
            var UI = new UserInterface();

            // inserting dummy data

            marketplace.Users.Add(new Seller("ante", "ante@gmail.com"));
            marketplace.Users.Add(new Seller("jure", "jure@gmail.com"));

            marketplace.Users.Add(new Customer("lovro", "lovro@gmail.com", 100.0));
            marketplace.Users.Add(new Customer("marko", "marko@gmail.com", 200.0));

            marketplace.Products.Add(
                new Product("piletina", "piletina", 5.30, (Seller)marketplace.Users.Find(x => x.Username == "ante"), ProductCategory.Food));
            marketplace.Products.Add(
                new Product("čokolada", "mliječna čokolada", 2.50, (Seller)marketplace.Users.Find(x => x.Username == "ante"), ProductCategory.Food));

            marketplace.Products.Add(
                new Product("Ryzen 5 7950x", "procesor", 500, (Seller)marketplace.Users.Find(x => x.Username == "jure"), ProductCategory.Electronics));
            marketplace.Products.Add(
                new Product("rtx 4090ti", "grafička kartica", 1038, (Seller)marketplace.Users.Find(x => x.Username == "jure"), ProductCategory.Electronics));

            var quit = false;
            while (!quit)
            {
                if (UI.MainMenu(marketplace) == 0)
                    quit = true;
            }
        }
    }
}
