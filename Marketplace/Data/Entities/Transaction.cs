using Marketplace.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Customer Costumer { get; set; }
        public Seller Seller { get; set; }
        public Guid ProductId { get; set; }
        public PromoCode PromoCode { get; set; }
        public DateTime DateTime { get; set; }
        public TransactionStatus Status { get; set; }
        public double MoneyPaidAtPurchase { get; set; }

        public Transaction(Customer costumer, Seller seller, Guid productId, PromoCode promoCode, DateTime dateTime, double money)
        {
            Id = Guid.NewGuid();
            Costumer = costumer;
            Seller = seller;
            ProductId = productId;
            PromoCode = promoCode;
            DateTime = dateTime;
            Status = TransactionStatus.Completed;
            MoneyPaidAtPurchase = money;
        }
    }
}
