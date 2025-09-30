using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.ViewModels
{
    public class CartItemViewModel
    {
        public int ItemId { get; set; }     
        public int ProductId { get; set; }   // ID produktu z katalogu
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
