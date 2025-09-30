using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Requests
{
    public record class UpdateItemQtyRequest
    {
        [Range(1, int.MaxValue)]
        public int Quantity { get; init; }
    }
}
