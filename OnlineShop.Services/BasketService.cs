using OnlineShop.Models;
using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services
{
    public class BasketService
    {
        public BasketService()
        {
            BasketData = new BasketData();
        }

        private BasketData BasketData { get; }

        public async Task<Basket> CreateBasketAsync(string userId)
        {
            return await BasketData.CreateBasketAsync(userId);
        }

        public async Task<Basket> GetBasketAsync(string userId)
        {
            return await BasketData.GetBasketAsync(userId);
        }

        public async Task AddItemAsync(BuyProductRequest productRequest)
        {
            await BasketData.AddItemAsync(productRequest.UserId, productRequest.ProductName);            
        }

        public async Task SetItemQuantityAsync(string userId, string productName, int? quantity)
        {
           await BasketData.SetItemQuantityAsync(userId, productName, quantity ?? 0);
        }

        public async Task DeleteItemAsync(string userId, string productName)
        {
            await BasketData.DeleteItemAsync(userId, productName);
        }

        public async Task DeleteAllItemsAsync(string userId)
        {
            await BasketData.DeleteAllItemsAsync(userId);
        }

    }
}
