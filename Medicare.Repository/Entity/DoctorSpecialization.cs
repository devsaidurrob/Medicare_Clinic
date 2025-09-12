using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Entity
{
    public class DoctorSpecialization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}
