using EShop.Domain.Models;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using ShoppingCart.Infrastructure.Repositories;
using CatalogProduct = EShop.Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }
        public Cart GetCart(int cartId)
        {
            var cart = _repository.FindById(cartId);
            if (cart is null)
            {
                cart = new Cart { Id = cartId };
                _repository.Add(cart);
            }
            return cart;
        }

        public List<Cart> GetAllCarts() => _repository.GetAll();

        public void AddProductToCart(int cartId, CatalogProduct product)
        {
            var (pid, name, price) = MapCatalogProduct(product);

            var item = new CartItem
            {
                ProductId = pid,
                ProductName = name,
                UnitPrice = price,
                Quantity = 1
            };

            AddCartItem(cartId, item);
        }
        public void AddCartItem(int cartId, CartItem item)
        {
            if (item.Quantity <= 0) throw new ArgumentOutOfRangeException(nameof(item.Quantity));

            var cart = GetCart(cartId);

            // MERGE po ProductId – w koszyku max jedna pozycja danego produktu
            var existing = cart.Items.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (existing is null)
            {
                item.Id = NextItemId(cart);
                cart.Items.Add(item);
            }
            else
            {
                existing.Quantity += item.Quantity;
                if (!string.IsNullOrWhiteSpace(item.ProductName)) existing.ProductName = item.ProductName;
                if (item.UnitPrice > 0) existing.UnitPrice = item.UnitPrice;
            }

            _repository.Update(cart);
        }

        public void UpdateProductQuantity(int cartId, int itemId, int quantity)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            var cart = GetCart(cartId);
            var item = cart.Items.FirstOrDefault(p => p.Id == itemId)
                       ?? throw new KeyNotFoundException("Item not found");

            item.Quantity = quantity;
            _repository.Update(cart);
        }

        public void RemoveProductFromCart(int cartId, int itemId)
        {
            var cart = GetCart(cartId);
            var item = cart.Items.FirstOrDefault(p => p.Id == itemId);
            if (item != null)
            {
                cart.Items.Remove(item);
                _repository.Update(cart);
            }
        }

        public void ClearCart(int cartId)
        {
            var cart = GetCart(cartId);
            cart.Items.Clear();
            _repository.Update(cart);
        }

        public decimal GetTotal(int cartId) => GetCart(cartId).Total;

        private static int NextItemId(Cart cart)
            => cart.Items.Count == 0 ? 1 : cart.Items.Max(p => p.Id) + 1;

        private static (int productId, string name, decimal unitPrice) MapCatalogProduct(CatalogProduct p)
        {
           
            var t = p.GetType();

            int id = Read<int>(t, p, "Id");
            string name = Read<string>(t, p, "Name")
                          ?? Read<string>(t, p, "ProductName")
                          ?? string.Empty;

            decimal price = Read<decimal>(t, p, "Price");
            if (price <= 0) price = Read<decimal>(t, p, "UnitPrice");

            return (id, name, price);
        }

        private static T? Read<T>(Type t, object obj, string prop)
        {
            var pi = t.GetProperty(prop);
            if (pi is null) return default;
            var val = pi.GetValue(obj);
            if (val is null) return default;
            return (T)Convert.ChangeType(val, typeof(T));
        }
    }
}
