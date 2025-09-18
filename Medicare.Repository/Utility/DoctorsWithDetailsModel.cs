using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Utility
{
    public class DoctorsWithDetailsModel
    {
        public Guid Id { get; set; }
        public string? DoctorName { get; set; }
        //public string Departments { get; set; }
        public string? Specializations { get; set; } 
    }
}
