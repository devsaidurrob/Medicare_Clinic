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
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly MedicareDbContext _context;

        public SpecializationRepository(MedicareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _context.Specializations
                .ToListAsync();
        }

        public async Task<Specialization> GetByIdAsync(int id)
        {
            return await _context.Specializations
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Specialization specialization)
        {
            await _context.Specializations.AddAsync(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Specialization specialization)
        {
            _context.Specializations.Update(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var spec = await _context.Specializations.FindAsync(id);
            if (spec != null)
            {
                _context.Specializations.Remove(spec);
                await _context.SaveChangesAsync();
            }
        }
    }
}
