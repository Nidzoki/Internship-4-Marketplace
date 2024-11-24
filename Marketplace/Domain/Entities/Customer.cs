using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entities
{
    public class Costumer : User
    {
        public double Balance { get; set; }
        public List<Product> PurchasedProducts { get; set; }
        public List<Product> FavoriteProducts { get; set; }


        public Costumer(string username, string email, double balance) : base(username, email)
        {
            Balance = balance;
            PurchasedProducts = new List<Product>();
            FavoriteProducts = new List<Product>();
        }

        public void BuyProduct()
        {
            throw new NotImplementedException();
        }

        public void ReturnProduct()
        {
            throw new NotImplementedException();
        }
    }
}
