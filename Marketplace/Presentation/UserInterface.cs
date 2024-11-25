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

        private int LogIn(Market marketplace)
        {   
            var profile = GetUserInput.LogInUser(marketplace);
            if(profile == null)
                return 0;
            if (profile is Customer)
                DisplayCustomerAccount(profile as Customer);
            else
                DisplaySellerAccount(profile as Seller);

            return 0;
        }

        public int DisplaySellerAccount(Seller profile)
        {
            Console.WriteLine($" Hello, {profile.Username}! Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplayCustomerAccount(Customer profile)
        {
            Console.WriteLine($" Hello, {profile.Username}!\n Balance: {profile.Balance} Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int Exit() => 0;
    }
}
