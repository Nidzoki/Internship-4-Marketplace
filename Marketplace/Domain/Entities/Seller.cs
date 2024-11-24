using Marketplace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entities
{
    public class Seller : User
    {
        public List<Product> Products { get; set; }

        public Seller(string ime, string email) : base(ime, email)
        {
            Products = new List<Product>();
        }

        public void CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product, Product newData)
        {
            throw new NotImplementedException();
        }

        public double GetProfit()
        {
            throw new NotImplementedException();
        }
    }
}
