using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Username { get; set; } = default!;

        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }             //required lub przypisanie default jak wyzej

        [Required]
        public string PasswordHash { get; set; } = default!;          

        public ICollection<UserRole>? Roles { get; set; } // = new List<UserRole>();       

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
