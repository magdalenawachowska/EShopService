using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string RoleName { get; set; } = default!;

        public User User { get; set; } = default!;
    }
}
