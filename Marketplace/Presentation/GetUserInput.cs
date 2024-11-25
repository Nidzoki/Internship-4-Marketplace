using Marketplace.Data.Entities;
using Marketplace.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Presentation
{
    public static class GetUserInput
    {
        // GET BASIC USER INFO SECTION

        public static string GetEmail(Market marketplace)
        {
            var mail = string.Empty;
            var askForMail = true;

            while (askForMail)
            {
                Console.Write(" Enter your email or x to cancel: ");
                mail = Console.ReadLine().Trim();

                if (mail == "x")
                    return null;

                var valid = ValidateInput.ValidateEmail(mail);
                var notTaken = ValidateInput.IsEmailAvailable(mail, marketplace);

                if (valid && notTaken)
                    askForMail = false;
                else
                {
                    Console.WriteLine(!valid
                        ? "\n ERROR! Email not valid."
                        : "\n ERROR! Email is already in use. Please log in using this email or create account using different email.");
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                }
            }
            return mail;
        }

        public static string GetUsername(Market marketplace)
        {
            var username = string.Empty;
            var askForUsername = true;

            while (askForUsername)
            {
                Console.Write(" Enter your username or x to cancel: ");
                username = Console.ReadLine().Trim();

                if (username == "x")
                    return null;

                var valid = ValidateInput.ValidateUsername(username);
                var notTaken = ValidateInput.IsUsernameAvailable(username, marketplace);

                if (valid && notTaken)
                    askForUsername = false;
                else
                {
                    Console.WriteLine(!valid
                        ? "\n ERROR! Username not valid. It has to be 3-20 characters long, and contain only letters, numbers and underscores."
                        : "\n ERROR! Username taken. Please choose another username.");
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                }
            }
            return username;
        }

        public static double GetBalance()
        {
            double balance;

            do
            {
                Console.Write(" Enter starting balance or x to cancel: ");
                var input = Console.ReadLine().Trim();
                if (input.ToLower() == "x")
                    return -1;
                if (!double.TryParse(input, out balance) || balance < 0)
                {
                    Console.WriteLine("\n ERROR! Input has to be a positive number!");
                    balance = -1; // Ensure balance stays negative to keep looping
                }
            } while (balance < 0);

            return balance;
        }

        // REGISTER SECTION

        public static Seller RegisterSeller(Market marketplace)
        {
            Console.Clear();
            Console.WriteLine("\n REGISTER AS SELLER\n\n");

            string username = GetUsername(marketplace);
            if (username == null)
                return null;
            string mail = GetEmail(marketplace);
            return mail != null ? new Seller(username, mail) : null;
        }

        public static Customer RegisterCustomer(Market marketplace)
        {
            Console.Clear();
            Console.WriteLine("\n REGISTER AS CUSTOMER\n\n");
            string username = GetUsername(marketplace);
            if (username == null)
                return null;
            string mail = GetEmail(marketplace);
            if (mail == null)
                return null;
            double balance = GetBalance();
            return balance >= 0 ? new Customer(username, mail, balance) : null;
        }
    }
}