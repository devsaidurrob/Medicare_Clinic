using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Entity
{
    public class DoctorsEducation
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key to Doctor
        [Required]
        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }

        // Degree Name (MBBS, MD, etc.)
        [Required]
        [StringLength(100)]
        public string Degree { get; set; }

        // University or Institution
        [Required]
        [StringLength(200)]
        public string Institution { get; set; }

        // Field of Study / Specialization
        [StringLength(150)]
        public string FieldOfStudy { get; set; }

        // Year of Completion
        public int YearOfCompletion { get; set; }

        // Additional Notes (optional)
        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
