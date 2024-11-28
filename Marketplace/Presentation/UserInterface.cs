﻿using Marketplace.Data.Entities;
using Marketplace.Data.Enums;
using Marketplace.Domain.TransactionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Presentation
{   
    public class UserInterface
    {
        public delegate int MyDelegate(Market argument);

        public delegate int MyProfileDelegate(Market argument, User user);

        public int MainMenu(Market marketplace)
        {
            Dictionary<string, MyDelegate> mainMenuOptionHandler = new Dictionary<string, MyDelegate>()
            {
                {"1", RegisterMenu },
                {"2", LogIn }
            };
            
            var askForOption = true;
            var option = string.Empty;

            while (askForOption)
            {
                Printer.PrintMainMenu();
                option = Console.ReadLine().Trim();

                if (option == "3")
                    return 0;

                if (!mainMenuOptionHandler.Keys.Contains(option))
                {
                    Printer.PrintInputError();
                    return -1;
                }
                askForOption = false;
            }
            var runChosenMenu = true;
            do
            {
                if (mainMenuOptionHandler[option].Invoke(marketplace) == 0)
                    runChosenMenu = false;
            }
            while (runChosenMenu);

            return 1;
        }

        public int RegisterMenu(Market marketplace)
        {
            Dictionary<string, MyDelegate> registerMenuOptionHandler = new Dictionary<string, MyDelegate>()
            {
                {"1", RegisterAsCostumer},
                {"2", RegisterAsSeller }
            };

            var askForOption = true;
            var option = string.Empty;

            while (askForOption)
            {
                Printer.PrintRegisterMenu();
                option = Console.ReadLine();

                if (option == "3")
                    return 0;

                if (!registerMenuOptionHandler.Keys.Contains(option))
                {
                    Printer.PrintInputError();
                    return -1;
                }
                askForOption = false;
            }

            var runChosenMenu = true;
            do{
                if (registerMenuOptionHandler[option].Invoke(marketplace) == 0)
                    runChosenMenu = false;
            }
            while (runChosenMenu);
            
            return 1;
        }

        public int RegisterAsSeller(Market marketplace)
        {
            var sellerData = GetUserInput.RegisterSeller(marketplace);

            if (sellerData != null)
            {
                marketplace.Users.Add(sellerData);
                Console.Clear();
                Console.WriteLine("\n REGISTER AS SELLER\n\n Account successfully created!\n\n Press any key to continue...");
                Console.ReadKey();
            }
            return 0;
        }

        public int RegisterAsCostumer(Market marketplace)
        {
            var customerData = GetUserInput.RegisterCustomer(marketplace);

            if (customerData != null)
            {
                marketplace.Users.Add(customerData);
                Console.Clear();
                Console.WriteLine("\n REGISTER AS CUSTOMER\n\n Account successfully created!\n\n Press any key to continue...");
                Console.ReadKey();
            }
            return 0;
        }

        public int LogIn(Market marketplace)
        {   
            var profile = GetUserInput.LogInUser(marketplace);
            if(profile == null)
                return 0;
            if (profile is Customer)
                while(DisplayCustomerAccount(profile as Customer, marketplace) != 0);
            else
                while(DisplaySellerAccount(profile as Seller, marketplace) != 0);

            return 0;
        }

        // SELLER SECTION

        public int DisplaySellerAccount(Seller profile, Market marketplace)
        {
            Dictionary<string, MyProfileDelegate> sellerAccountOptionHandler = new Dictionary<string, MyProfileDelegate>()
            {
                {"1", AddProduct },
                {"2", DisplaySellerProducts },
                {"3", DisplayProfit },
                {"4", DisplaySoldProductsByCategory },
                {"5", DisplayProfitInTimeInterval }
            };

            var askForOption = true;
            var option = string.Empty;

            while (askForOption)
            {
                Printer.PrintSellerView(profile.Username);
                option = Console.ReadLine().Trim();

                if (option == "6")
                    return 0;

                if (!sellerAccountOptionHandler.Keys.Contains(option))
                {
                    Printer.PrintInputError();
                    return -1;
                }
                askForOption = false;
            }
            var runChosenMenu = true;
            do
            {
                if (sellerAccountOptionHandler[option].Invoke(marketplace, profile) == 0)
                    runChosenMenu = false;
            }
            while (runChosenMenu);

            return 1;
        }

        public int DisplayProfitInTimeInterval(Market marketplace, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplaySoldProductsByCategory(Market marketplace, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplayProfit(Market marketplace, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplaySellerProducts(Market marketplace, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n YOUR PRODUCT LIST\n");
            foreach(var product in marketplace.Products.Where(x => x.Seller == seller))
            {
                Printer.PrintProduct(product);
                Console.WriteLine();
            }
            Console.ReadKey();
            return 0;
        }

        public int AddProduct(Market marketplace, User seller)
        {
            var newProduct = GetUserInput.GetNewProductData(seller as Seller);

            if (newProduct != null)
            {
                marketplace.Products.Add(newProduct);
                Console.Clear();
                Console.WriteLine("\n Product creation succesfully completed.\n\n Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Product creation abandoned.\n\n Press any key to continue...");
                Console.ReadKey();
            }
            return 0;
        }

        // CUSTOMER SECTION

        public int DisplayCustomerAccount(Customer profile, Market marketplace)
        {
            Dictionary<string, MyProfileDelegate> customerAccountOptionHandler = new Dictionary<string, MyProfileDelegate>()
            {
                {"1", DisplayAllProducts },
                {"2", BuyProduct },
                {"3", ReturnProduct },
                {"4", AddToFavorites },
                {"5", DisplayShoppingHistory },
                {"6", DisplayFavorites }
            };

            var askForOption = true;
            var option = string.Empty;

            while (askForOption)
            {
                Printer.PrintCustomerView(profile);
                option = Console.ReadLine().Trim();

                if (option == "7")
                    return 0;

                if (!customerAccountOptionHandler.Keys.Contains(option))
                {
                    Printer.PrintInputError();
                    return -1;
                }
                askForOption = false;
            }
            var runChosenMenu = true;
            do
            {
                if (customerAccountOptionHandler[option].Invoke(marketplace, profile) == 0)
                    runChosenMenu = false;
            }
            while (runChosenMenu);

            return 1;
        }

        public int DisplayFavorites(Market argument, User user)
        {
            Console.Clear();
            Console.WriteLine("\n FAVORITE PRODUCTS\n");

            if (((Customer)user).FavoriteProducts.Count == 0)
            {
                Console.WriteLine(" There is no products to display.\n\n Press any key to continue...");
                Console.ReadKey();
                return 0;
            }

            foreach (var product in ((Customer)user).FavoriteProducts)
                Printer.PrintProductShort(product);

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();

            return 0;
        }

        public int DisplayShoppingHistory(Market argument, User user)
        {
            Console.Clear();
            Console.WriteLine("\n SHOPPING HISTORY\n");

            if (((Customer)user).PurchasedProducts.Count == 0)
            {
                Console.WriteLine(" There is no products to display.\n\n Press any key to continue...");
                Console.ReadKey();
                return 0;
            }

            foreach (var product in ((Customer)user).PurchasedProducts)
                Printer.PrintProductShort(product);

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();

            return 0;
        }

        public int AddToFavorites(Market marketplace, User user)
        {
            Console.Clear();
            Console.WriteLine("\n ADD TO FAVORITES\n\n");

            var products = marketplace.Products.Where(x => x.Status == ProductStatus.Available).Except(((Customer)user).FavoriteProducts).ToList();

            if (products.Count == 0)
            {
                Console.WriteLine(" There are no available products to add to favorites.");
                Console.ReadKey();
                return 0;
            }

            for (var i = 0; i < products.Count(); i++)
                Console.WriteLine($" Option:{i + 1}\n\t Name: {products[i].Name}\n\t Category: {products[i].Category}\n\t Seller: {products[i].Seller.Username}\n");

            while (true)
            {
                Console.Write("\n Select an option to add to favorites or enter x to cancel: ");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "x")
                    return 0;

                if (int.TryParse(input, out int selectedOption) && selectedOption > 0 && selectedOption <= products.Count)
                {
                    var selectedProduct = products[selectedOption - 1];
                    ((Customer)user).FavoriteProducts.Add(selectedProduct);
                    Console.WriteLine($"\nProduct '{selectedProduct.Name}' has been added to your favorites.");
                    return 0;
                }
                else
                    Console.WriteLine("Invalid selection. Please try again.");

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public int ReturnProduct(Market argument, User user)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int BuyProduct(Market marketplace, User user)
        {
            Console.Clear();
            Console.WriteLine("\n BUY PRODUCT\n\n");

            var products = marketplace.Products.Where(x => x.Status == ProductStatus.Available).ToList();

            if (products.Count == 0)
            {
                Console.WriteLine(" There are no available products to buy.");
                Console.ReadKey();
                return 0;
            }

            for (var i = 0; i < products.Count(); i++)
                Console.WriteLine($" Option:{i + 1}\n\t Name: {products[i].Name}\n\t Category: {products[i].Category}\n\t Seller: {products[i].Seller.Username}\n");

            while (true)
            {
                Console.Write("\n Select a product to buy or enter x to cancel: ");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "x")
                    return 0;

                if (int.TryParse(input, out int selectedOption) && selectedOption > 0 && selectedOption <= products.Count)
                {
                    var selectedProduct = products[selectedOption - 1];

                    if (TransactionManager.CreateTransaction(marketplace, (Customer)user, selectedProduct, new PromoCode("", 10, ProductCategory.Food, DateTime.Now)))
                        Console.WriteLine($"\nProduct '{selectedProduct.Name}' has been purchased.\n\n Press any key to continue...");
                    else
                        Console.WriteLine($" Insufficient balance to buy this product!\n\n Press any key to continue...");
                    
                    Console.ReadKey();
                    return 0;
                }
                else
                    Console.WriteLine("Invalid selection. Please try again.");

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public int DisplayAllProducts(Market marketplace, User user) // user is here to satisfy the delegate function
        {
            Console.Clear();
            Console.WriteLine("\n ALL PRODUCTS LIST\n");
            foreach (var product in marketplace.Products.Where(x => x.Status == ProductStatus.Available))
            {
                Printer.PrintProduct(product);
            }
            Console.ReadKey();
            return 0;
        }
    }
}
