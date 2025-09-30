using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; } = new();   
        public decimal Total => Items.Sum(i => i.UnitPrice *i.Quantity);
        public string CustomerEmail { get; set; } = string.Empty;
    }
}
