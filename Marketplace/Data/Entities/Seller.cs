using Marketplace.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Entities
{
    public class Seller : User
    {
        public List<Product> Products { get; set; }
        public double Profit { get; private set; }

        public Seller(string ime, string email) : base(ime, email)
        {
            Products = new List<Product>();
        }

        public double GetProfit(double profit) => Profit += profit;
    }
}
