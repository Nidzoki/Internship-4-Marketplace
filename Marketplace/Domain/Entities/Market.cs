using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entities
{
    public class Market
    {
        public List<User> Users { get; private set; }
        public List<Product> Products { get; private set; }
        public List<PromoCode> PromoCodes { get; private set; }
        public List<Transaction> Transactions { get; private set; }
        public double ProvisionIncome { get; private set; }
        

        public Market()
        {
            Users = new List<User>();
            Products = new List<Product>();
            PromoCodes = new List<PromoCode>();
            Transactions = new List<Transaction>();
            ProvisionIncome = 0;
        }

        public void RegisterUser(User user) => Users.Add(user);

        public void DeleteUser(User user) => Users.Remove(user);


    }
}
