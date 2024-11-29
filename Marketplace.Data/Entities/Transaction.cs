using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Data.Enums;

namespace Marketplace.Data.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid CostumerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid ProductId { get; set; }
        public PromoCode PromoCode { get; set; }
        public DateTime DateTime { get; set; }
        public TransactionStatus Status { get; set; }
        public double MoneyPaidAtPurchase { get; set; }

        public Transaction(Guid costumerId, Guid sellerId, Guid productId, PromoCode promoCode, DateTime dateTime, double money)
        {
            Id = Guid.NewGuid();
            CostumerId = costumerId;
            SellerId = sellerId;
            ProductId = productId;
            PromoCode = promoCode;
            DateTime = dateTime;
            Status = TransactionStatus.Completed;
            MoneyPaidAtPurchase = money;
        }
    }
}
