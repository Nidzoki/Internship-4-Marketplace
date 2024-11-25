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
        public Product Product { get; set; }
        public PromoCode PromoCode { get; set; }
        public DateTime DateTime { get; set; }

        public Transaction(Customer costumer, Seller seller, Product product, PromoCode promoCode, DateTime dateTime)
        {
            Id = Guid.NewGuid();
            Costumer = costumer;
            Seller = seller;
            Product = product;
            PromoCode = promoCode;
            DateTime = dateTime;
        }
    }
}
