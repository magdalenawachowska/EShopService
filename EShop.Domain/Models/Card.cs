using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models
{
    public class Card
    {
        public int Id { get; set; }
        public required string CardNumber { get; set; }
    }
}
