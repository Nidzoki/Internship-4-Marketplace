using Marketplace.Data.Entities;
using Marketplace.Data.Enums;
using Marketplace.Domain;
using Marketplace.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Marketplace.Presentation
{
    public class UserInterface
    {
        public delegate int MyDelegate(Data.Program.Context argument);

        public delegate int MyProfileDelegate(Data.Program.Context argument, User user);

        public int MainMenu(Data.Program.Context marketplace)
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

            while (mainMenuOptionHandler[option].Invoke(marketplace) != 0) ;

            return 1;
        }

        public int RegisterMenu(Data.Program.Context marketplace)
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

            while (registerMenuOptionHandler[option].Invoke(marketplace) != 0);

            return 1;
        }

        public int RegisterAsSeller(Data.Program.Context marketplace)
        {
            var sellerData = GetUserInput.RegisterSeller(marketplace);

            if (sellerData != null)
            {
                var userRepo = new UserRepository(marketplace);
                userRepo.AddUser(sellerData);
                Console.Clear();
                Console.WriteLine("\n REGISTER AS SELLER\n\n Account successfully created!\n\n Press any key to continue...");
                Console.ReadKey();
            }
            return 0;
        }

        public int RegisterAsCostumer(Data.Program.Context marketplace)
        {
            var customerData = GetUserInput.RegisterCustomer(marketplace);

            if (customerData != null)
            {
                var userRepo = new UserRepository(marketplace);
                userRepo.AddUser(customerData);
                Console.Clear();
                Console.WriteLine("\n REGISTER AS CUSTOMER\n\n Account successfully created!\n\n Press any key to continue...");
                Console.ReadKey();
            }
            return 0;
        }

        public int LogIn(Data.Program.Context marketplace)
        {
            var profile = GetUserInput.LogInUser(marketplace);
            if (profile == null)
            {
                Console.Clear();
                Console.WriteLine("The user with these credentials does not exist. Please try again.\n\n Press any key to continue...");
                Console.ReadKey();
                return 1;
            }
            if (profile is Customer)
                while (DisplayCustomerAccount(profile as Customer, marketplace) != 0) ;
            else
                while (DisplaySellerAccount(profile as Seller, marketplace) != 0) ;

            return 0;
        }

        // SELLER SECTION

        public int DisplaySellerAccount(Seller profile, Data.Program.Context marketplace)
        {
            Dictionary<string, MyProfileDelegate> sellerAccountOptionHandler = new Dictionary<string, MyProfileDelegate>()
            {
                {"1", AddProduct },
                {"2", DisplaySellerProducts },
                {"3", DisplayProfit },
                {"4", DisplaySoldProductsByCategory },
                {"5", DisplayProfitInTimeInterval },
                {"6", EditProductPrice }
            };

            var askForOption = true;
            var option = string.Empty;

            while (askForOption)
            {
                Printer.PrintSellerView(profile.Username);
                option = Console.ReadLine().Trim();

                if (option == "7")
                    return 0;

                if (!sellerAccountOptionHandler.Keys.Contains(option))
                {
                    Printer.PrintInputError();
                    return -1;
                }
                askForOption = false;
            }

            while (sellerAccountOptionHandler[option].Invoke(marketplace, profile) != 0) ;

            return 1;
        }

        public int EditProductPrice(Data.Program.Context marketplace, User user)
        {
            Console.Clear();
            Console.WriteLine("\n EDIT PRODUCT PRICE\n\n");

            var productRepo = new ProductRepository(marketplace);

            var products =
                productRepo.GetProductList()
                .Where
                (
                    x => x.Status == ProductStatus.Available
                    && x.Seller.Username == user.Username
                )
                .ToList();

            if (products.Count == 0)
            {
                Console.WriteLine(" There are no available products to add to favorites.\n\n Press any key to continue...");
                Console.ReadKey();
                return 0;
            }

            for (var i = 0; i < products.Count(); i++)
                Console.WriteLine($" Option:{i + 1}\n\t Name: {products[i].Name}\n\t Category: {products[i].Category}\n\t Price: {products[i].Price}\n");

            while (true)
            {
                Console.Write("\n Select an option to edit price or enter x to cancel: ");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "x")
                    return 0;

                if (int.TryParse(input, out int selectedOption) && selectedOption > 0 && selectedOption <= products.Count)
                {
                    var selectedProduct = products[selectedOption - 1];
                    var newPrice = GetUserInput.GetNewProductPrice();

                    if (newPrice < 0)
                        return 0;

                    productRepo.UpdatePrice(selectedProduct.Id, newPrice);

                    Console.WriteLine($"\nProduct '{selectedProduct.Name}' price has been changed to {newPrice}.\n\n Press any key to continue...");
                    return 0;
                }

                Console.WriteLine("Invalid selection. Please try again.\n\n Press any key to continue...");
                Console.ReadKey();
            }
        }

        public int DisplayProfitInTimeInterval(Data.Program.Context marketplace, User seller)
        {
            Console.Clear();

            var interval = GetUserInput.GetTimeInterval();
            var transactionRepo = new TransactionRepository(marketplace);
            var selectedTransactions =
                transactionRepo.GetTransactionList()
                .Where
                (
                    x => x.SellerId == seller.Id
                    && Domain.Validation.Validation.CheckIfInInterval(interval, x.DateTime)
                )
                .ToList();

            var completedTransactionProductIds =
                selectedTransactions
                .Where(x => x.Status == TransactionStatus.Completed)
                .Select(x => x.ProductId);

            var revertedTransactionProductIds =
                selectedTransactions
                .Where(x => x.Status == TransactionStatus.Reverted)
                .Select(x => x.ProductId);

            var profit = 0.0;

            foreach (var productId in completedTransactionProductIds)
                profit += 0.95 * transactionRepo.GetTransactionList().Find(x => x.ProductId == productId).MoneyPaidAtPurchase;

            foreach (var productId in revertedTransactionProductIds)
                profit += 0.15 * transactionRepo.GetTransactionList().Find(x => x.ProductId == productId).MoneyPaidAtPurchase;

            Console.WriteLine($" \n PROFIT IN SELECTED INTERVAL\n\n" +
                $" Start date: {interval.Start:dd-MM-yyyy}\n" +
                $" End date: {interval.End:dd-MM-yyyy}\n\n" +
                $" Your profit: {Math.Round(profit, 2)}\n\n Press any key to continue...");
            Console.ReadKey();

            return 0;
        }

        public int DisplaySoldProductsByCategory(Data.Program.Context marketplace, User seller)
        {
            Console.Clear();
            var productCategory = GetUserInput.GetProductCategory();
            Console.Clear();
            Console.WriteLine("\n SOLD PRODUCTS LIST\n");

            var productRepo = new ProductRepository(marketplace);
            var selectedProducts =
                productRepo.GetProductList()
                .Where(
                    x => x.Seller == seller &&
                    x.Category == productCategory &&
                    x.Status == ProductStatus.SoldOut);

            if (selectedProducts.Count() == 0)
            {
                Console.WriteLine("There is no products to display.\n\n Press any key to continue...");
                Console.ReadKey();
                return 0;
            }

            foreach (var product in selectedProducts)
            {
                Printer.PrintProduct(product);
                Console.WriteLine();
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplayProfit(Data.Program.Context marketplace, User seller)
        {
            Console.Clear();
            Console.WriteLine($"\n YOUR PROFIT\n\n Your current profit is: {((Seller)seller).Profit}\n\n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int DisplaySellerProducts(Data.Program.Context marketplace, User seller)
        {
            Console.Clear();
            Console.WriteLine("\n YOUR PRODUCT LIST\n");

            var productRepo = new ProductRepository(marketplace);
            foreach (var product in productRepo.GetProductList().Where(x => x.Seller == seller))
            {
                Printer.PrintProduct(product);
                Console.WriteLine();
            }
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }

        public int AddProduct(Data.Program.Context marketplace, User seller)
        {
            var newProduct = GetUserInput.GetNewProductData(seller as Seller);

            if (newProduct != null)
            {
                var productRepo = new ProductRepository(marketplace);
                productRepo.AddProduct(newProduct);
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

        public int DisplayCustomerAccount(Customer profile, Data.Program.Context marketplace)
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

            while (customerAccountOptionHandler[option].Invoke(marketplace, profile) != 0) ;

            return 1;
        }

        public int DisplayFavorites(Data.Program.Context marketplace, User user)
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

        public int DisplayShoppingHistory(Data.Program.Context marketplace, User user)
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

        public int AddToFavorites(Data.Program.Context marketplace, User user)
        {
            Console.Clear();
            Console.WriteLine("\n ADD TO FAVORITES\n\n");

            var productRepo = new ProductRepository(marketplace);
            var products = productRepo.GetProductList().Where(x => x.Status == ProductStatus.Available).Except(((Customer)user).FavoriteProducts).ToList();

            if (products.Count == 0)
            {
                Console.WriteLine(" There are no available products to add to favorites.\n\n Press any key to continue...");
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

        public int ReturnProduct(Data.Program.Context marketplace, User user)
        {
            Console.Clear();
            Console.WriteLine("\n RETURN PRODUCT\n");

            var products = ((Customer)user).PurchasedProducts.ToList();

            if (products.Count == 0)
            {
                Console.WriteLine(" There is no products to display.\n\n Press any key to continue...");
                Console.ReadKey();
                return 0;
            }

            for (var i = 0; i < products.Count; i++)
                Console.WriteLine($" Option:{i + 1}\n\t Name: {products[i].Name}\n\t Category: {products[i].Category}\n\t Seller: {products[i].Seller.Username}\n");

            while (true)
            {
                Console.Write("\n Select a product to return or enter x to cancel: ");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "x")
                    return 0;

                if (int.TryParse(input, out int selectedOption) && selectedOption > 0 && selectedOption <= products.Count)
                {
                    var selectedProduct = products[selectedOption - 1];
                    var transactionRepo = new Domain.Repositories.TransactionRepository(marketplace);
                    transactionRepo.RevertTransaction(marketplace.Transactions.Find(x => x.ProductId == selectedProduct.Id).Id);

                    Console.WriteLine($"\nProduct '{selectedProduct.Name}' has been returned.\n\n Press any key to continue...");
                    Console.ReadKey();

                    return 0;
                }
                else
                    Console.WriteLine("Invalid selection. Please try again.");

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public int BuyProduct(Data.Program.Context marketplace, User user)
        {
            Console.Clear();
            Console.WriteLine("\n BUY PRODUCT\n\n");

            var productRepo = new ProductRepository(marketplace); 
            var products = productRepo.GetProductList().Where(x => x.Status == ProductStatus.Available).ToList();

            if (products.Count == 0)
            {
                Console.WriteLine(" There are no available products to buy.\n\n Press any key to continue...");
                Console.ReadKey();
                return 0;
            }

            for (var i = 0; i < products.Count(); i++)
                Console.WriteLine($" Option:{i + 1}\n\t Name: {products[i].Name}\n\t Category: {products[i].Category}\n\t Seller: {products[i].Seller.Username}\n\t Price: {products[i].Price}\n");

            while (true)
            {
                Console.Write("\n Select a product to buy or enter x to cancel: ");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "x")
                    return 0;

                if (int.TryParse(input, out int selectedOption) && selectedOption > 0 && selectedOption <= products.Count)
                {
                    var selectedProduct = products[selectedOption - 1];
                    var promocode = GetUserInput.GetPromoCode();
                    var promocodeData = marketplace.PromoCodes.Find(x => x.Code == promocode);
                    var transactionRepo = new TransactionRepository(marketplace);
                    if (transactionRepo.CreateTransaction((Customer)user, selectedProduct, promocodeData))
                        Console.WriteLine($"\nProduct '{selectedProduct.Name}' has been purchased.\n\n Press any key to continue...");
                    else
                        Console.WriteLine($" Insufficient balance to buy this product!\n\n Press any key to continue...");

                    Console.ReadKey();
                    return 0;
                }

                Console.WriteLine("Invalid selection. Please try again.\n\n Press any key to continue...");
                Console.ReadKey();
            }
        }

        public int DisplayAllProducts(Data.Program.Context marketplace, User user) // user is here to satisfy the delegate function
        {
            Console.Clear();
            Console.WriteLine("\n ALL PRODUCTS LIST\n");
            
            var productRepo = new ProductRepository(marketplace);
            foreach (var product in productRepo.GetProductList().Where(x => x.Status == ProductStatus.Available))
            {
                Printer.PrintProduct(product);
            }
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}
