using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Entity
{
    public class Doctor
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? ProfessionStartDate { get; set; }
        public string?  DisplayTitle { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public bool Deleted { get; set; } = false;

        public virtual ICollection<Appointment> Appointments { get; set; }

        // Many-to-many with Department
        public virtual ICollection<DoctorDepartment> DoctorDepartments { get; set; }

        // Many-to-many with Specialization
        public virtual ICollection<DoctorSpecialization> DoctorSpecializations { get; set; }
        public virtual ICollection<DoctorsEducation> Educations { get; set; }
    }
}
