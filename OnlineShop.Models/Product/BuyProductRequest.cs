using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class BuyProductRequest
    {

        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

    }
}
