using System;

namespace Module6HW5.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
