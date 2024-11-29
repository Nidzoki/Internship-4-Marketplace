using Marketplace.Data.Entities;
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

        public static void PrintInputError()
        {
            Console.Clear();
            Console.WriteLine("\n ERROR! Input is not correct! Please try again.\n\n Press any key to continue...");
            Console.ReadKey();
        }

        public static void PrintRegisterMenu()
        {
            Console.Clear();
            Console.WriteLine("\n REGISTER MENU\n\n 1. Register as customer\n 2. Register as seller\n 3. Back");
            Console.Write("\n Your input: ");
        }

        public static void PrintSellerView(string sellerName)
        {
            Console.Clear();
            Console.WriteLine($"\n Hello, {sellerName}!\n" +
                $"\n 1. Add new product to sell" +
                $"\n 2. Display my products" +
                $"\n 3. Display profit" +
                $"\n 4. Display sold products by category" +
                $"\n 5. Display profit in selected time interval" +
                $"\n 6. Edit product price" +
                $"\n 7. Log out");
            Console.Write("\n Your input: ");
        }

        public static void PrintCustomerView(Customer profile)
        {
            Console.Clear();
            Console.WriteLine($"\n Hello, {profile.Username}!\n\n Account ballance: {profile.Balance}" +
                $"\n 1. Display all products for sale" +
                $"\n 2. Buy product using ID" +
                $"\n 3. Return product" +
                $"\n 4. Add product to favorites" +
                $"\n 5. Display shopping history" +
                $"\n 6. Display favorite products" +
                $"\n 7. Log out");
            Console.Write("\n Your input: ");
        }

        public static void PrintProduct(Product product)
        {
            Console.WriteLine($" Name: {product.Name}" +
                $"\n\tID: {product.GetProductId()}" +
                $"\n\tCategory: {product.Category}" +
                $"\n\tStatus: {product.Status}" +
                $"\n\tPrice: {product.Price}" +
                $"\n\tDescription: {product.Description}" +
                $"\n\tReviews: {product.GetRating()} ({product.Reviews.Count})");
        }

        public static void PrintProductShort(Product product)
        {
            Console.WriteLine($" \tName: {product.Name} | Category: {product.Category} | Seller: {product.Seller.Username}\n");
        }
    }
}
