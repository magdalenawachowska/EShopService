using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartService : ICartReader, ICartAdder, ICartRemover
    {
        void AddCartItem(int cartId, CartItem item);
        void UpdateProductQuantity(int cartId, int itemId, int quantity);
        void ClearCart(int cartId);
        decimal GetTotal(int cartId);
    }
}
