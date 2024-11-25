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
            Reviews = new List<int>();
        }

        public Guid GetProductId() => Id;

        public double GetRating() => Reviews.Average();
    }

    public enum ProductStatus
    {
        Available,
        SoldOut
    }

    public enum ProductCategory
    {
        Electronics,
        Clothing,
        Books,
        Sports,
        Beauty,
        Toys,
        Health,
        Jewelry,
        Music,
        Garden,
        Food,
        Tools,
        Furniture,
        Software,
        Art
    }
}
