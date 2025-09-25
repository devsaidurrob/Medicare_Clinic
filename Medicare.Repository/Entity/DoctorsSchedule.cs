using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Entity
{
    public class DoctorsSchedule
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int? PatientCapacity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Doctor Doctor { get; set; }

        // Optional: list of appointments booked against this schedule
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
