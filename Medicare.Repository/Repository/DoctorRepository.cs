using Medicare.Repository.DbContext;
using Medicare.Repository.Entity;
using Medicare.Repository.Interfaces;
using Medicare.Repository.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Medicare.Repository.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MedicareDbContext _context;

        public DoctorRepository(MedicareDbContext context)
        {
            _context = context;
        }

        public async Task<PagingResult<Doctor>> GetAllAsync(DoctorFilter filter)
        {
            var query = _context.Doctors.AsQueryable();

            // Apply filters
            if (filter.Specialty != null)
                query = query.Where(d => d.DoctorSpecializations.Any(ds => ds.SpecializationId == filter.Specialty));

            //if (!string.IsNullOrEmpty(filter.Data.City))
            //    query = query.Where(d => d.City == filter.Data.City);

            // Apply search
            //if (!string.IsNullOrEmpty(filter.Search))
            //    query = query.Where(d => $"{d.FirstName} {d.LastName}".Contains(filter.Search));

            // Apply sorting
            if (!string.IsNullOrEmpty(filter.SortColumn))
            {
                // Example with System.Linq.Dynamic.Core
                var sortExpr = $"{filter.SortColumn} {filter.SortDir}";
                query = query.OrderBy(sortExpr);
            }

            var total = await query.CountAsync();

            var records = await query
                .Skip(filter.Start)
                .Take(filter.Length)
                .ToListAsync();

            return new PagingResult<Doctor>
            {
                Total = total,
                Records = records
            };
        }

        public async Task<Doctor?> GetByIdAsync(Guid id)
        {
            return await _context.Doctors
                .Include(d => d.Appointments)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            doctor.CreatedAt = DateTime.UtcNow;
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<Doctor?> UpdateAsync(Doctor doctor)
        {
            var existingDoctor = await _context.Doctors.FindAsync(doctor.Id);
            if (existingDoctor == null) return null;

            existingDoctor.FirstName = doctor.FirstName;
            existingDoctor.LastName = doctor.LastName;
            existingDoctor.Gender = doctor.Gender;
            existingDoctor.DateOfBirth = doctor.DateOfBirth;
            existingDoctor.Email = doctor.Email;
            existingDoctor.Phone = doctor.Phone;
            existingDoctor.Address = doctor.Address;
            existingDoctor.RegistrationNumber = doctor.RegistrationNumber;
            existingDoctor.DepartmentId = doctor.DepartmentId;
            existingDoctor.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingDoctor;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return false;

            doctor.Deleted = true;
            int retVal = await _context.SaveChangesAsync();
            return retVal > 0;
        }
    }
}
