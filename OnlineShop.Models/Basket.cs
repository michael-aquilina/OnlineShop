using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public IList<BasketItem> Items { get; set; }

        public int Total { get; set; }

        public Basket()
        {
            Items = new List<BasketItem>();
        }
    }
}
