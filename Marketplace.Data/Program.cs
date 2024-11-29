using Marketplace.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data
{
    public static class Program
    {
        public class Context
        {
            public List<User> Users { get; set; } = Seed.Users;
            public List<Product> Products { get; set; } = Seed.Products;
            public List<PromoCode> PromoCodes { get; set; } = Seed.PromoCodes;
            public List<Transaction> Transactions { get; set; } = Seed.Transactions;
            public double ProvisionIncome { get; set; } = Seed.ProvisionIncome;
        }

        static void Main()
        {

        }
    }
}
