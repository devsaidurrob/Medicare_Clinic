using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Utility
{
    public class DataTableFilter
    {
        // DataTable paging/sorting/search params
        public int Start { get; set; }
        public int Length { get; set; }
        public string SortColumn { get; set; } = string.Empty;
        public string SortDir { get; set; } = "asc";
        public string Search { get; set; } = string.Empty;
    }
    public class DoctorFilter : DataTableFilter
    {
        public int? Specialty { get; set; }
    }
    public class AppointmentFilter : DataTableFilter
    {
        public string? PatientName { get; set; }
        public Guid? DoctorId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
    public class UserFilter:DataTableFilter
    {
        public bool? Active { get; set; }
    }
}
