using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Entities
{
    public class Customer : User
    {
        public double Balance { get; set; }
        public List<Product> PurchasedProducts { get; set; }
        public List<Product> FavoriteProducts { get; set; }


        public Customer(string username, string email, double balance) : base(username, email)
        {
            Balance = balance;
            PurchasedProducts = new List<Product>();
            FavoriteProducts = new List<Product>();
        }
    }
}
