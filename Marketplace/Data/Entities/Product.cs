using Marketplace.Data.Enums;
using Marketplace.Domain.ReviewGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Entities
{
    public class Product
    {
        private Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Seller Seller { get; set; }
        public ProductStatus Status { get; set; }
        public ProductCategory Category { get; set; }
        public List<int> Reviews { get; set; }

        public Product(string name, string description, double price, Seller seller, ProductCategory category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Status = ProductStatus.Available;
            Seller = seller;
            Category = category;
            Reviews = ReviewGenerator.GetReviews();
        }

        public Guid GetProductId() => Id;

        public double GetRating() => Reviews.Count == 0 ? 0 : Math.Round(Reviews.Average(), 2);
    }
}
