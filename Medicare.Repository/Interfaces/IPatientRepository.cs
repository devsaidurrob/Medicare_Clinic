using Medicare.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> AddAsync(Patient patient);
        Task<Patient?> GetById(Guid Id);
        Task<IEnumerable<Patient>> SearchPatient(string searchTerm);
    }
}
