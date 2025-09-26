using Medicare.Repository.Entity;
using Medicare.Repository.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<PagingResult<Appointment>> GetAllAsync(AppointmentFilter filter);
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<Appointment> AddAsync(Appointment appointment);
        Task<Appointment?> UpdateAsync(Appointment appointment);
        Task<bool> DeleteAsync(Guid id);
        Task<List<string>> GetAvailableTimeSlots(Guid doctorId, DateTime date);
    }
}
