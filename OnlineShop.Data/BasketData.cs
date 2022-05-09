using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Core;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class BasketData
    {
        private ProductData ProductData;

        public BasketData()
        {
            ProductData = new ProductData();
        }

        public async Task<Basket> GetBasketAsync(string userId)
        {
            Basket basket = null;
            var connection = DataShared.GetConnection();
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT Id, Identifier
                FROM Basket
                WHERE lower(Identifier) = lower($userId)
                ";
                command.Parameters.AddWithValue("$userId", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        basket = new Basket();
                        basket.Id = reader.GetInt32(0);
                        basket.Identifier = reader.GetString(1);
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

            if (basket != null)
            {
                basket.Items = await GetBasketItems(userId);
                basket.Total = basket.Items.Sum(i => i.TotalPrice);
            }

            return basket;
        }


        public async Task<IList<BasketItem>> GetBasketItems(string basketId)
        {
            var basketItems = new List<BasketItem>();
            var connection = DataShared.GetConnection();
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT 	bi.[Id]
                          ,	bi.[ProductId] 
                          , bi.[Quantity]
                          , p.Title
                          , p.Price
                          , p.UrlName
                          , p.Description
                          , p.Image
                      FROM [BasketItem] bi
                      INNER JOIN Product p 
                        ON p.Id = bi.ProductId
                      INNER JOIN Basket b 
                        ON b.Id = bi.BasketId
                      WHERE b.Identifier = $basketId
                ";
                command.Parameters.AddWithValue("$basketId", basketId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        BasketItem bi = new BasketItem();
                        bi.Id = reader.GetInt32(0);
                        bi.ProductId = reader.GetInt32(1);
                        bi.Quantity = reader.GetInt32(2);
                        bi.Title = reader.GetString(3);
                        bi.Price = reader.GetInt32(4);
                        bi.ProductName = reader.GetString(5);
                        bi.Image = reader.GetString(7);
                        bi.TotalPrice = bi.Price * bi.Quantity;
                        basketItems.Add(bi);
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
            return basketItems;
        }

        public async Task<Basket> CreateBasketAsync(string userId)
        {
            // check if user has a bag already
            var basket = await GetBasketAsync(userId);
            if (basket != null)
            {
                return basket;
            }

            basket = new Basket()
            {
                Identifier = userId
            };

            var connection = DataShared.GetConnection();
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO Basket
                        (Identifier)
                        VALUES (@Identifier)";
                command.Parameters.AddWithValue("@Identifier", userId);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return await GetBasketAsync(userId);
        }

        /// <summary>
        /// Add an item to the backed of the given userid
        /// </summary>
        /// <param name="userId">User where the basket belongs to</param>
        /// <param name="item">Item to add</param>
        /// <returns></returns>
        public async Task<Basket> AddItemAsync(string userId, string productName, int quantity = 1)
        {
            var basket = await GetBasketAsync(userId);

            if (basket == null)
            {
                basket = await CreateBasketAsync(userId);
            }

            var product = ProductData.GetProduct(productName);

            if (product == null)
            {
                throw new Exception("Product not found for name '" + productName + "'");
            }

            var connection = DataShared.GetConnection();

            try
            {
                connection.Open();

                // we either add it as a new item
                // or we add to the already listed quantity of the item
                var basketItem = basket.Items.FirstOrDefault(i => i.ProductName.EqualsIgnoreCase(productName));
                if (basketItem == null)

                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                        @"INSERT INTO [BasketItem]
                        (   [ProductId]
                           ,[BasketId]
                           ,[Quantity]
                        )
                        VALUES (@ProductId, @BasketId, @Quantity)";
                    command.Parameters.AddWithValue("@ProductId", product.Id);
                    command.Parameters.AddWithValue("@BasketId", basket.Id);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    await command.ExecuteNonQueryAsync();
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
            return await GetBasketAsync(userId);
        }

        public async Task SetItemQuantityAsync(string userId, string productId, int quantity)
        {
            var basket = await GetBasketAsync(userId);

            if (basket == null)
            {
                basket = await CreateBasketAsync(userId);
            }

            if (quantity == 0)
            {
                await DeleteItemAsync(userId, productId);
            }
            else
            {


                var connection = DataShared.GetConnection();

                try
                {
                    connection.Open();

                    // we either add it as a new item
                    // or we add to the already listed quantity of the item
                    var basketItem = basket.Items.FirstOrDefault(i => i.ProductName.EqualsIgnoreCase(productId));
                    if (basketItem == null)
                    {
                        await AddItemAsync(userId, productId, quantity);
                    } 
                    else
                    {
                        var command = connection.CreateCommand();
                        command.CommandText =
                                @"UPDATE [BasketItem] 
                                set [Quantity] = $quantity 
                                where ID=$id";
                        command.Parameters.AddWithValue("$quantity", quantity);
                        command.Parameters.AddWithValue("$id", basketItem.Id);
                        await command.ExecuteNonQueryAsync();
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
            }
        }

        public async Task DeleteAllItemsAsync(string userId)
        {
            var basket = await GetBasketAsync(userId);

            if (basket == null)
            {
                return;
            }

            var connection = DataShared.GetConnection();

            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                        @"DELETE FROM [BasketItem]  
                            where BasketId=$basketId";
                command.Parameters.AddWithValue("$basketId", basket.Id);
                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task DeleteItemAsync(string userId, string productId)
        {
            var basket = await GetBasketAsync(userId);

            if (basket == null)
            {
                basket = await CreateBasketAsync(userId);
            }

            var connection = DataShared.GetConnection();

            try
            {
                connection.Open();

                // we either add it as a new item
                // or we add to the already listed quantity of the item
                var basketItem = basket.Items.FirstOrDefault(i => i.ProductName.EqualsIgnoreCase(productId));
                if (basketItem != null)
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                            @"DELETE FROM [BasketItem]  
                            where ID=$id";
                    command.Parameters.AddWithValue("$id", basketItem.Id);
                    await command.ExecuteNonQueryAsync();
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
        }

    }
}
