using Medicare.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(Guid id);
        Task<Doctor> AddAsync(Doctor doctor);
        Task<Doctor?> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(Guid id);
    }
}
