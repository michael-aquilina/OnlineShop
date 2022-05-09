using OnlineShop.Models;
using OnlineShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        public BasketService BasketService { get; }
        public ProductService ProductService { get; }

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            BasketService = new BasketService();
            ProductService = new ProductService();
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }

        [HttpGet("{id}")]
        public Product Get(string id)
        {
            return ProductService.GetProduct(id);
        }

        [HttpPost]
        [Route("buy")]
        public async Task Buy(BuyProductRequest request)
        {

            string userId = Request.Headers["userId"];

            if (userId == null)
            {
                throw new UnauthorizedAccessException("userId not in cookie");
            }
            request.UserId = userId;
            await BasketService.AddItemAsync(request);

            return;

        }
    }
}
