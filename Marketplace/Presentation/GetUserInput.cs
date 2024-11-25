using Marketplace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Presentation
{
    public static class GetUserInput
    {
        public static Seller RegisterSeller(Market marketplace)
        {
            var username = string.Empty;
            var askForUsername = true;

            while (askForUsername)
            {
                Console.Clear();
                Console.WriteLine("\n REGISTER AS SELLER\n\n");
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

            var mail = string.Empty;
            var askForMail = true;

            while (askForMail)
            {
                Console.Clear();
                Console.WriteLine("\n REGISTER AS SELLER\n\n");
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
            return new Seller(username, mail);
        }
    }
}