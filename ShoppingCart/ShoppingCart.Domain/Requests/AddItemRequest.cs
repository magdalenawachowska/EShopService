using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Requests
{
    public record class AddItemRequest
    {
        [Required] public int ProductId { get; init; }
        [Required, MaxLength(200)] public string ProductName { get; init; } = string.Empty;
        [Range(0.01, double.MaxValue)] public decimal UnitPrice { get; init; }
        [Range(1, int.MaxValue)] public int Quantity { get; init; }
    }
}
