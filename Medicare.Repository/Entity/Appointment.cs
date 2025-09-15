using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Entity
{
    public class Appointment
    {
        [Key]
        public System.Guid Id { get; set; }
        public System.Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public int? DepartmentId { get; set; }
        public System.DateTime AppointmentDate { get; set; }
        public System.TimeSpan? AppointmentTime { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public System.DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Guid CreatedUser { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Department? Department { get; set; }
    }
}
