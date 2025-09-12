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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MedicareDbContext _context;

        public DoctorRepository(MedicareDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
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

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
