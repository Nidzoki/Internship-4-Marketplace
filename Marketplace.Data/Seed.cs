using Marketplace.Data.Entities;
using Marketplace.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data
{
    public static class Seed
    {
        public static readonly List<User> Users = new List<User>()
        {
            new Seller("ante", "ante@gmail.com"),
            new Seller("jure", "jure@gmail.com"),
            new Seller("mate", "mate@gmail.com"),
            new Customer("lovro", "lovro@gmail.com", 10000),
            new Customer("vinko", "vinko@gmail.com", 250),
            new Customer("marko", "marko@gmail.com", 536)
        };

        public static readonly List<Product> Products = new List<Product>()
        {
            new Product("piletina", "piletina", 5.30, new Seller("ante", "ante@gmail.com"), ProductCategory.Food),
            new Product("cokolada", "cokolada", 8.2, new Seller("ante", "ante@gmail.com"), ProductCategory.Food),
            new Product("rtx4090ti", "graficka kartica", 1200, new Seller("jure", "jure@gmail.com"), ProductCategory.Electronics),
            new Product("Arduino Uno R3", "mikrokontroler", 15, new Seller("mate", "mate@gmail.com"), ProductCategory.Electronics),
            new Product("Raspberry Pi 4", "mikroračunalo", 50, new Seller("ante", "ante@gmail.com"), ProductCategory.Electronics),
            new Product("Cube", "bicikl", 600, new Seller("ante", "ante@gmail.com"), ProductCategory.Toys),
        };

        public static readonly List<PromoCode> PromoCodes = new List<PromoCode>()
        {
            new PromoCode("VALID_ELECTRONICS", 0.1, ProductCategory.Electronics, DateTime.Now.AddDays(1)),
            new PromoCode("INVALID_ELECTRONICS", 0.2, ProductCategory.Electronics, DateTime.Now.AddDays(-1))
        };

        public static readonly List<Transaction> Transactions = new List<Transaction>();

        public static readonly double ProvisionIncome = 0;
    }
}