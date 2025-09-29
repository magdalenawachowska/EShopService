namespace EShop.Domain.Models
{
    public class BaseModel
    {                     
        public bool Deleted { get; set; } = false;                         //okreslenie aktywnosci produktu
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid UpdatedBy { get; set; }                               //identyfikator edytora

    }
}
