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
    }
}
