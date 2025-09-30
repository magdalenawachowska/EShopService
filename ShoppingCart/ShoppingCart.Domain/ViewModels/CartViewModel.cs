using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.ViewModels
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public decimal Total { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new();
    }
}
