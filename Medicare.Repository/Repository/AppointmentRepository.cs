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

namespace Medicare.Repository.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly MedicareDbContext _context;

        public AppointmentRepository(MedicareDbContext context)
        {
            _context = context;
        }

        public async Task<PagingResult<Appointment>> GetAllAsync(AppointmentFilter filter)
        {
            var query = _context.Appointments
                .Include(d => d.Doctor)
                .Include(p => p.Patient)
                .Include(d => d.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.PatientName))
                query = query.Where(a => $"{a.Patient.FirstName} {a.Patient.LastName}".Contains(filter.PatientName));

            if (filter.DoctorId.HasValue)
                query = query.Where(a => a.DoctorId == filter.DoctorId);

            if (filter.DateFrom.HasValue)
                query = query.Where(a => a.AppointmentDate >= filter.DateFrom);

            if (filter.DateTo.HasValue)
                query = query.Where(a => a.AppointmentDate <= filter.DateTo);

            var total = await query.CountAsync();

            var data = await query.ToListAsync();

            return new PagingResult<Appointment>
            {
                Total = total,
                Records = data
            };
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments
                .Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Appointment> AddAsync(Appointment model)
        {
            await _context.Appointments.AddAsync(model);
            //await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Appointment?> UpdateAsync(Appointment model)
        {
            var entity = await _context.Appointments.FindAsync(model.Id);
            if (entity == null) return null;

            entity.PatientId = model.PatientId;
            entity.DoctorId = model.DoctorId;
            entity.AppointmentDate = model.AppointmentDate;
            entity.Status = model.Status;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Appointments.FindAsync(id);
            if (entity == null) return false;

            _context.Appointments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
