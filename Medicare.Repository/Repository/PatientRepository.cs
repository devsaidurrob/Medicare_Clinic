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

        public async Task<Patient?> GetById(Guid Id)
        {
            var data = await _context.Patients.FindAsync(Id);
            return data;
        }

        public async Task<IEnumerable<Patient>> SearchPatient(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Enumerable.Empty<Patient>();

            searchTerm = searchTerm.Trim().ToLower();

            return await _context.Patients
                .Where(p =>
                    (!string.IsNullOrEmpty(p.MobileNumber) && p.MobileNumber.Contains(searchTerm)) ||
                    (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.ToLower().Contains(searchTerm)) ||
                    (!string.IsNullOrEmpty(p.LastName) && p.LastName.ToLower().Contains(searchTerm)) ||
                    (!string.IsNullOrEmpty(p.Email) && p.Email.ToLower().Contains(searchTerm))
                )
                .OrderBy(p => p.FirstName)
                .ToListAsync();
        }
    }
}
