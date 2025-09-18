using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Entity
{
    public class DoctorDepartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
