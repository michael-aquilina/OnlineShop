using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services
{
    public class ProductService
    {
        public ProductService()
        {
            ProductData = new ProductData();
        }

        public ProductData ProductData { get; }

        public IList<Product> GetProducts()
        {
            return this.ProductData.GetProducts();
        }

        public Product GetProduct(string urlName)
        {
            return this.ProductData.GetProduct(urlName);
        }

    }
}
