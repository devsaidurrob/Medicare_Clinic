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
    public class DoctorsScheduleRepository : IDoctorsScheduleRepository
    {
        private readonly MedicareDbContext _context;

        public DoctorsScheduleRepository(MedicareDbContext context)
        {
            _context = context;
        }
        public async Task<DoctorsSchedule?> AddAsync(DoctorsSchedule schedule)
        {
            await _context.DoctorsSchedules.AddAsync(schedule);
            int retVal = await _context.SaveChangesAsync();
            if (retVal == 0) return null;
            return schedule;
        }

        public async Task<bool> DeleteAsync(Guid DoctorId)
        {
            var result = await _context.DoctorsSchedules.FindAsync(DoctorId);
            if (result == null) return false;
            _context.Remove(result);
            int retVal = await _context.SaveChangesAsync();
            if (retVal > 0) return true;
            return false;
        }

        public async Task<IEnumerable<DoctorsSchedule>> GetAllAsync(Guid DoctorId)
        {
            var result = await _context.DoctorsSchedules.Where(x => x.DoctorId == DoctorId).ToListAsync();
            return result;
        }
    }
}
