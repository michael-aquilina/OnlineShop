using OnlineShop.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.UnitTest
{
    public class BasketUnitTest
    {
        string userId = "";
        BasketService basketService;
        ProductService productService;

        public BasketUnitTest()
        {
            userId = Guid.NewGuid().ToString();
            basketService = new BasketService();
            productService = new ProductService();
        }

        [Fact]
        public async Task TestCreate()
        {
            var basket = await basketService.CreateBasketAsync(userId);
            Assert.NotNull(basket);
        }

        [Fact]
        public async Task TestAddItem()
        {
            var items = productService.GetProducts();
            var product = items.First();

            await basketService.AddItemAsync(new Models.BuyProductRequest()
            {
                ProductName = product.UrlName,
                UserId = userId
            });

            var basket = await basketService.GetBasketAsync(userId);
            
            // check if basket was created
            Assert.NotNull(basket);

            // check if there are any items
            Assert.True(basket.Items.Any());

            // check if the item added is the one in question
            Assert.True(basket.Items.First().ProductName == product.UrlName);

        }

        [Fact]
        public async Task TestDeleteItem()
        {
            var items = productService.GetProducts();
            var product = items.First();

            await basketService.AddItemAsync(new Models.BuyProductRequest()
            {
                ProductName = product.UrlName,
                UserId = userId
            });

            await basketService.DeleteItemAsync(userId, product.UrlName);

            var basket = await basketService.GetBasketAsync(userId);

            // check if basket was created
            Assert.NotNull(basket);
            
            // check if the item deleted is not in the list
            Assert.DoesNotContain(basket.Items, p => p.ProductName == product.UrlName);

        }

        [Fact]
        public async Task TestAddQuantity()
        {
            var items = productService.GetProducts();
            var product = items.First();
            var quantity = 5;

            await basketService.AddItemAsync(new Models.BuyProductRequest()
            {
                ProductName = product.UrlName,
                UserId = userId
            });

            await basketService.SetItemQuantityAsync(userId, product.UrlName, quantity);

            var basket = await basketService.GetBasketAsync(userId);

            // check if basket was created
            Assert.NotNull(basket);

            // check if the item is in the list at the given quantity is in the list
            Assert.Contains(basket.Items, p => p.ProductName == product.UrlName && p.Quantity == quantity);

        }

        [Fact]
        public async Task TestClearItems()
        {
            var items = productService.GetProducts();
            var product = items.First();
            var quantity = 5;

            await basketService.AddItemAsync(new Models.BuyProductRequest()
            {
                ProductName = product.UrlName,
                UserId = userId
            });

            var basket = await basketService.GetBasketAsync(userId);

            // check if basket was created
            Assert.NotNull(basket);

            // check if there are items
            Assert.NotEmpty(basket.Items);

            // clear the items
            await basketService.DeleteAllItemsAsync(userId);

            basket = await basketService.GetBasketAsync(userId);

            // check if there are no items
            Assert.Empty(basket.Items);

        }
    }
}