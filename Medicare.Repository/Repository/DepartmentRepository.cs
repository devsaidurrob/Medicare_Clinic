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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MedicareDbContext _context;

        public DepartmentRepository(MedicareDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments
                .ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept != null)
            {
                _context.Departments.Remove(dept);
                await _context.SaveChangesAsync();
            }
        }
    }
}
