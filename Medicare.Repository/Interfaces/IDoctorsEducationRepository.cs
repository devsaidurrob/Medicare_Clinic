using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medicare.Repository.Entity;

namespace Medicare.Repository.Interfaces
{
    public interface IDoctorsEducationRepository
    {
        Task<DoctorsEducation?> AddAsync(DoctorsEducation education);
        Task<DoctorsEducation> GetByIdAsync(int id);
        Task<DoctorsEducation> UpdateAsync(DoctorsEducation education);
        Task<bool> DeleteAsync(int id);
    }
}
