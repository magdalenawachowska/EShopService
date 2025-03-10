namespace EShopService.Models
{
    public class BaseModel
    {                     
        public bool Deleted { get; set; } = false;                         //okreslenie aktywnosci produktu
        public DateTime Created_at { get; set; } = DateTime.UtcNow;        //data utowrzenia rekordu
        public Guid Created_by { get; set; }                               //identyfikator autora
        public DateTime Updated_at { get; set; } = DateTime.UtcNow;        //data ostatniej aktualizacji rekordu
        public Guid Updated_by { get; set; }                               //identyfikator edytora

    }
}
