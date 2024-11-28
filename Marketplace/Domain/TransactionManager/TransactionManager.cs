using Marketplace.Data.Entities;
using Marketplace.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.TransactionManager
{
    public static class TransactionManager
    {
        public static List<Transaction> TransactionList = new List<Transaction>();

        public static bool CreateTransaction(Market marketplace, Customer customer, Product product, PromoCode promoCode)
        {
            if (customer.Balance < product.Price)
                return false;

            TransactionList.Add(new Transaction(customer, product.Seller, product.GetProductId(), promoCode, DateTime.Now));

            customer.PurchasedProducts.Add(product);
            customer.Balance -= product.Price;

            marketplace.TakeTransactionProvision(product.Price * 0.05);

            product.Status = ProductStatus.SoldOut;

            ((Seller)marketplace.Users.Find(x => x.Username == product.Seller.Username)).GetProfit(0.95 * product.Price);

            return true;
        }

        public static void RevertTransaction(Market marketplace, Transaction transaction)
        {
            var transactionToRevert = TransactionList.Find(x => x.Id == transaction.Id);
            var customer = (Customer)marketplace.Users.Find(x => x.Username == transaction.Costumer.Username);
            var seller = (Seller)marketplace.Users.Find(x => x.Username == transaction.Seller.Username);
            var product = marketplace.Products.Find(x => x.GetProductId() == transaction.ProductId);

            transactionToRevert.Status = TransactionStatus.Reverted;

            customer.PurchasedProducts.Remove(marketplace.Products.Find(x => x.GetProductId() == transaction.ProductId));
            customer.Balance += 0.8 * marketplace.Products.Find(x => x.GetProductId() == transaction.ProductId).Price;
            
            seller.GetProfit(-0.8 * marketplace.Products.Find(x => x.GetProductId() == transaction.ProductId).Price);

            product.Status = ProductStatus.Available;
        }
    }
}
