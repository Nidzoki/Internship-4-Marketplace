using Marketplace.Data.Entities;
using Marketplace.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Repositories
{
    public class TransactionRepository
    {
        private readonly Data.Program.Context marketplace;

        public TransactionRepository(Data.Program.Context marketplaceContext)
        {
            marketplace = marketplaceContext;
        }

        public bool CreateTransaction(Customer customer, Product product, PromoCode promoCode)
        {
            var discount = marketplace.PromoCodes.Contains(promoCode) && Validation.Validation.ValidatePromoCode(promoCode) ? promoCode.Discount : 0.0;

            if (customer.Balance < product.Price * (1 - discount))
                return false;

            marketplace.Transactions.Add(new Transaction(customer.Id, product.Seller.Id, product.Id, promoCode, DateTime.Now, product.Price * (1 - discount)));

            customer.PurchasedProducts.Add(product);
            customer.Balance -= product.Price * (1 - discount);

            marketplace.ProvisionIncome += product.Price * (1 - discount) * 0.05;

            product.Status = ProductStatus.SoldOut;

            ((Seller)marketplace.Users.Find(x => x.Username == product.Seller.Username)).Profit += 0.95 * product.Price * (1 - discount);

            return true;
        }

        public void RevertTransaction(Guid transactionId)
        {
            var transactionToRevert = marketplace.Transactions.Find(x => x.Id == transactionId);
            var customer = (Customer)marketplace.Users.Find(x => x.Id == transactionToRevert.CostumerId);
            var seller = (Seller)marketplace.Users.Find(x => x.Id == transactionToRevert.SellerId);
            var product = marketplace.Products.Find(x => x.Id == transactionToRevert.ProductId);
            var discount = transactionToRevert.PromoCode != null ? transactionToRevert.PromoCode.Discount : 0.0;

            transactionToRevert.Status = TransactionStatus.Reverted;

            customer.PurchasedProducts.Remove(marketplace.Products.Find(x => x.Id == transactionToRevert.ProductId));
            customer.Balance += 0.8 * marketplace.Products.Find(x => x.Id == transactionToRevert.ProductId).Price * (1 - discount);

            seller.Profit -= 0.8 * marketplace.Products.Find(x => x.Id == transactionToRevert.ProductId).Price * (1 - discount);

            product.Status = ProductStatus.Available;
        }

        public List<Transaction> GetTransactionList() => marketplace.Transactions;
    }
}
