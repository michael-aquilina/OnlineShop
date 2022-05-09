using OnlineShop.Models;
using OnlineShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketItemController : ControllerBase
    {
        public BasketService BasketService { get; }

        private readonly ILogger<BasketController> _logger;

        public BasketItemController(ILogger<BasketController> logger)
        {
            BasketService = new BasketService();
            _logger = logger;
        }

        [HttpPost]
        [Route("update")]
        public async Task UpdateBasketItem(BasketItemRequest request)
        {
            string userId = Request.Headers["userId"];

            if (userId == null)
            {
                throw new UnauthorizedAccessException("userId not in cookie");
            }
            await BasketService.SetItemQuantityAsync(userId, request.ProductName, request.Quantity);
        }

        [HttpPost]
        [Route("delete")]
        public async Task DeleteBasketItem(BasketItemRequest request)
        {
            string userId = Request.Headers["userId"];

            if (userId == null)
            {
                throw new UnauthorizedAccessException("userId not in cookie");
            }
            await BasketService.DeleteItemAsync(userId, request.ProductName);
        }
    }
}
