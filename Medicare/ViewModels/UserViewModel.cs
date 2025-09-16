using Medicare.Repository.Entity;
using System.ComponentModel.DataAnnotations;

namespace Medicare.ViewModels
{
    public class UserViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
    public class CreateUserViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(100)]
        [Required]
        public string UserName { get; set; }   // Login username

        [MaxLength(255)]
        [Required]
        public string Email { get; set; }      // Unique email
        //[Required]
        //public string Password { get; set; }   // Store hashed password

        [MaxLength(20)]
        [Required]
        public string? PhoneNumber { get; set; }

        // ✅ Multiple roles support
        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

        public bool IsActive { get; set; } = true;
        public List<Guid> UserRoles { get; set; }
    }
}
