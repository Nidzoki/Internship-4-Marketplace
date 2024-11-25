using Marketplace.Data.Entities;
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

        public int DisplayProfitInTimeInterval(Market argument, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplaySoldProductsByCategory(Market argument, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplayProfit(Market argument, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplaySellerProducts(Market argument, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int AddProduct(Market argument, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

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
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplayShoppingHistory(Market argument, User user)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int AddToFavorites(Market argument, User user)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int ReturnProduct(Market argument, User user)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int BuyProduct(Market argument, User user)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplayAllProducts(Market argument, User user)
        {
            Console.Clear();
            Console.WriteLine("\n This isn't implemented yet.\n \n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int Exit() => 0;
    }
}
