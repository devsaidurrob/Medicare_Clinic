using Medicare.Repository.Entity;
using Medicare.Repository.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Interfaces
{
    public interface IDoctorRepository
    {
        Task<PagingResult<Doctor>> GetAllAsync(DoctorFilter filter);
        Task<Doctor?> GetByIdAsync(Guid id);
        Task<Doctor> AddAsync(Doctor doctor);
        Task<Doctor?> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(Guid id);
    }
}
