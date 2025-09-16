using Medicare.Repository.DbContext;
using Medicare.Repository.Entity;
using Medicare.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MedicareDbContext _context;

        public PatientRepository(MedicareDbContext context)
        {
            _context = context;
        }
        public async Task<Patient> AddAsync(Patient patient)
        {
            patient.Id = Guid.NewGuid();
            await _context.Patients.AddAsync(patient);
            return patient;
        }
    }
}
