using Medicare.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Interfaces
{
    public interface IDoctorsScheduleRepository
    {
        Task<IEnumerable<DoctorsSchedule>> GetAllAsync(Guid DoctorId);
        Task<DoctorsSchedule?> AddAsync(DoctorsSchedule schedule);
        Task<bool> DeleteAsync(Guid DoctorId);
    }
}
