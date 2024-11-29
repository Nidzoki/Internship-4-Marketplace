using Marketplace.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Seller Seller { get; set; }
        public ProductStatus Status { get; set; }
        public ProductCategory Category { get; set; }

        public Product(string name, string description, double price, Seller seller, ProductCategory category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Status = ProductStatus.Available;
            Seller = seller;
            Category = category;
        }
    }
}
