using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Requests;
using ShoppingCart.Domain.ViewModels;
using System.Security.Claims;


namespace ShoppingCartService.Controllers
{
    [ApiController]
    [Route("api/carts/me")]
    [Authorize] 
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        public CartController(ICartService service) => _service = service;

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // GET /api/carts/me
        [HttpGet]
        public ActionResult<CartViewModel> GetMyCart()
        {
            var cart = _service.GetCart(UserId);
            return Ok(Map(cart));
        }

        // POST /api/carts/me/items
        [HttpPost("items")]
        public IActionResult AddItem([FromBody] AddItemRequest req)
        {
            var item = new CartItem
            {
                ProductId = req.ProductId,
                ProductName = req.ProductName,
                UnitPrice = req.UnitPrice,
                Quantity = req.Quantity
            };

            _service.AddCartItem(UserId, item);
            return Ok(Map(_service.GetCart(UserId)));
        }

        // PATCH /api/carts/me/items/{itemId}
        [HttpPatch("items/{itemId:int}")]
        public IActionResult UpdateQty([FromRoute] int itemId, [FromBody] UpdateItemQtyRequest req)
        {
            try
            {
                _service.UpdateProductQuantity(UserId, itemId, req.Quantity);
                return Ok(Map(_service.GetCart(UserId)));
            }
            catch (KeyNotFoundException) { return NotFound(new { error = "Item not found" }); }
            catch (ArgumentOutOfRangeException) {  return BadRequest(new { error = "Quantity must be >= 1" }); }
        }

        // DELETE /api/carts/me/items/{itemId}
        [HttpDelete("items/{itemId:int}")]
        public IActionResult RemoveItem([FromRoute] int itemId)
        {
            try
            {
                _service.RemoveProductFromCart(UserId, itemId);
                return Ok(Map(_service.GetCart(UserId)));
            }
            catch (KeyNotFoundException) { return NotFound(new { error = "Item not found" }); }
        }

        // DELETE /api/carts/me
        [HttpDelete]
        public IActionResult Clear()
        {
            _service.ClearCart(UserId);
            return NoContent();
        }

        // POST /api/carts/me/checkout
        [HttpPost("checkout")]
        public IActionResult Checkout()
        {
            var cart = _service.GetCart(UserId);
            var summary = new
            {
                total = cart.Total,
                items = cart.Items.Select(i => new
                {
                    itemId = i.Id,
                    productId = i.ProductId,
                    name = i.ProductName,
                    unitPrice = i.UnitPrice,
                    quantity = i.Quantity,
                    lineTotal = i.UnitPrice * i.Quantity
                })
            };
            return Ok(summary);
        }

        // mapowanie encji domeny 
        private static CartViewModel Map(Cart c) => new CartViewModel
        {
            CartId = c.Id,
            Total = c.Total,
            Items = c.Items.Select(i => new CartItemViewModel
            {
                ItemId = i.Id,
                ProductId = i.ProductId,
                Name = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                LineTotal = i.UnitPrice * i.Quantity
            }).ToList()
        };
    }
}
