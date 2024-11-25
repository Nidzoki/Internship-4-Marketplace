using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Entities
{
    public class PromoCode
    {
        public string Code { get; private set; }
        public double Discount { get; private set; }
        public ProductCategory Category { get; private set; }
        public DateTime ExpiryDate { get; private set; }

        public PromoCode(string code, double discount, ProductCategory category, DateTime expiryDate)
        {
            Code = code;
            Discount = discount;
            Category = category;
            ExpiryDate = expiryDate;
        }

        public bool IsValid() => ExpiryDate >= DateTime.Now;

        public double ApplyDiscount(Product product) => IsValid() && product.Category == Category ? product.Price - (product.Price * Discount / 100) : product.Price;
    }
}
