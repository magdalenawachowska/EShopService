using System.ComponentModel;

namespace EShopService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; } = default;
        public string? Ean { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }                                     //ilosc dostepnych sztuk
        public string? Sku { get; set; }                                   //Stock Keeping Unit- kod do przypisania do produktu w katalogu
        public Category? Category { get; set; }
        public bool Deleted { get; set; } = false;                         //okreslenie aktywnosci produktu
        public DateTime Created_at { get; set; } = DateTime.UtcNow;        //data utowrzenia rekordu
        public Guid Created_by { get; set; }
        public DateTime Updated_at { get; set; } = DateTime.UtcNow;        //data ostatniej aktualizacji rekordu
        public Guid Updated_by { get; set; }                               //identyfikator edytora




    }
}
