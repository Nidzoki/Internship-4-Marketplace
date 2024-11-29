using Marketplace.Data.Entities;
using Marketplace.Data.Enums;
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

        public static string GetExistingEmail(Market marketplace)
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

                if (valid && !notTaken)
                    askForMail = false;
                else
                {
                    Console.WriteLine(!valid
                        ? "\n ERROR! Email not valid."
                        : "\n ERROR! Email is not registered. Please log in using registered email or create new account.");
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                }
            }
            return mail;
        }

        private static string GetExistingUsername(Market marketplace)
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

                if (valid && !notTaken)
                    askForUsername = false;
                else
                {
                    Console.WriteLine(!valid
                        ? "\n ERROR! Username not valid. It has to be 3-20 characters long, and contain only letters, numbers and underscores."
                        : "\n ERROR! Username not registered.");
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                }
            }
            return username;
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

        // LOGIN SECTION

        public static User LogInUser(Market marketplace)
        {
            Console.Clear();
            Console.WriteLine("\n LOG IN\n\n");

            var email = GetExistingEmail(marketplace);
            if (email == null)
                return null;
            var username = GetExistingUsername(marketplace);
            if (username == null)
                return null;

            return marketplace.Users.Find(x => x.Username == username && x.Email == email);
        }

        // NEW PRODUCT DATA COLLECTION SECTION

        public static Product GetNewProductData(Seller seller)
        {
            var name = GetNewProductName();
            if (name == null) return null;

            var description = GetNewProductDescription();
            if (description == null) return null;

            var price = GetNewProductPrice();
            if (price < 0) return null;

            var category = GetProductCategory();
            if (category == null) return null;

            return new Product(name, description, price, seller, (ProductCategory)category);
        }

        private static string GetNewProductName()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("\n Enter product name or x to cancel: ");
                var productName = Console.ReadLine().Trim();

                if (productName == "x")
                    return null;

                if (string.IsNullOrEmpty(productName) || productName.Length < 3)
                {
                    Console.WriteLine("\n Invalid product name. It has to contain at least 3 characters. Please try again.\n\n Press any key to continue...");
                    Console.ReadKey();
                }
                return productName;
            }
        }

        public static string GetNewProductDescription()
        {
            Console.Clear();
            Console.Write("\n Enter product description or x to cancel: ");
            var productDescription = Console.ReadLine().Trim();

            return productDescription == "x" ? null : productDescription;
        }

        public static double GetNewProductPrice()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("\n Enter product price or x to cancel: ");
                var productPriceInput = Console.ReadLine().Trim();

                if (productPriceInput == "x")
                    return -1;

                if (!double.TryParse(productPriceInput, out var productPrice) || productPrice < 0)
                {
                    Console.WriteLine("\n Invalid product price. It has to be greater or equal to zero. Please try again.\n\n Press any key to continue...");
                    Console.ReadKey();
                }
                else
                    return productPrice;
            }
        }

        public static ProductCategory? GetProductCategory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n CHOOSE PRODUCT CATEGORY\n");

                var categories = Enum.GetValues(typeof(ProductCategory)).Cast<ProductCategory>().ToList();

                for (int i = 0; i < categories.Count; i++)
                    Console.WriteLine($"{i + 1}. {categories[i]}");

                Console.Write("\n Enter product number or x to cancel: ");
                var input = Console.ReadLine().Trim();

                if (input == "x")
                    return null;

                if (int.TryParse(input, out int categoryNumber) && categoryNumber >= 1 && categoryNumber <= categories.Count)
                    return categories[categoryNumber - 1];

                Console.WriteLine("Invalid input. Please enter a number corresponding to a category.\n\n Press any key to try again...");
                Console.ReadKey();
            }
        }

        // TIME INTERVAL SECTION

        public static Interval GetTimeInterval()
        {
            DateTime startDate = DateTime.Now, endDate = DateTime.Now;

            bool askForStartDate = true, askForEndDate = true;

            while (askForStartDate)
            {
                Console.Write(" Enter the start date (yyyy-MM-dd) or x to cancel: ");
                var input = Console.ReadLine().Trim();

                if (input.ToLower() == "x")
                    return null;

                if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out startDate))
                    askForStartDate = false;
                else
                {
                    Console.WriteLine("\n ERROR! Start date not valid. Please enter a valid date in the format yyyy-MM-dd.");
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                }
            }

            while (askForEndDate)
            {
                Console.Write(" Enter the end date (yyyy-MM-dd) or x to cancel: ");
                var input = Console.ReadLine().Trim();

                if (input.ToLower() == "x")
                    return null;

                if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out endDate) && endDate >= startDate)
                    askForEndDate = false;
                else
                {
                    Console.WriteLine("\n ERROR! End date not valid or earlier than start date. Please enter a valid date in the format yyyy-MM-dd that is after the start date.");
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                }
            }
            return new Interval(startDate, endDate);
        }

        // PROMO CODE SECTION

        public static string GetPromoCode()
        {
            Console.Write(" Enter promo code or leave empty: ");
            return Console.ReadLine().Trim();
        }
    }
}