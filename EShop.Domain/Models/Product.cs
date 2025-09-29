using System.ComponentModel;

namespace EShop.Domain.Models
{
    public class Product : BaseModel 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Ean { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; } = 0;                                   
        public string Sku { get; set; } = string.Empty;                      //Stock Keeping Unit- kod 
        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; }          
    }
}
