using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Entity
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }   // Login username

        [MaxLength(255)]
        public string Email { get; set; }      // Unique email

        public string PasswordHash { get; set; }   // Store hashed password

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        // ✅ Multiple roles support
        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

        public bool IsActive { get; set; } = true;
        public bool Deleted { get; set; } = false;
        // Auditing
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }

}
