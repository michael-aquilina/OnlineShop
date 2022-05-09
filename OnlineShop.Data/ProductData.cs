using OnlineShop.Core;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Data
{
    public class ProductData
    {
        public Product GetProduct(string urlName)
        {
            Product product = new Product();
            var connection = DataShared.GetConnection();
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT Title, UrlName, Description, Image, Price, Id
                FROM Product
                WHERE lower(UrlName) = lower($urlname)
            ";
                command.Parameters.AddWithValue("$urlName", urlName);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.Title = reader.GetString(0);
                        product.UrlName = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.Image = reader.GetString(3);
                        product.Price = reader.GetInt32(4);
                        product.Id = reader.GetInt32(5);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return product;
        }

        public IList<Product> GetProducts()
        {
            var connection = DataShared.GetConnection();
            IList<Product> products = new List<Product>();
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT Title, UrlName, Description, Image, Price, Id
                FROM Product
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.Title = reader.GetString(0);
                        product.UrlName = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.Image = reader.GetString(3);
                        product.Price = reader.GetInt32(4);
                        product.Id = reader.GetInt32(5);
                        products.Add(product);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return products;
        }
    }
}
