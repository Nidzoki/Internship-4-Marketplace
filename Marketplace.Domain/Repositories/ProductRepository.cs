using Marketplace.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Repositories
{
    public class ProductRepository
    {
        private readonly Data.Program.Context marketplace;

        public ProductRepository(Data.Program.Context marketplaceContext)
        {
            marketplace = marketplaceContext;
        }

        public void AddProduct(Product product) => marketplace.Products.Add(product);

        public Product GetProduct(Guid id) => marketplace.Products.Find(x => x.Id == id);

        public List<Product> GetProductList() => marketplace.Products;

        public void UpdatePrice(Guid id, double newPrice) 
            => marketplace.Products.Find(x => x.Id == id).Price = newPrice;
    }
}
