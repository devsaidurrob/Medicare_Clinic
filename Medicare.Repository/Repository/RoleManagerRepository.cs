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
    public class RoleManagerRepository : IRoleManagerRepository
    {
        private readonly MedicareDbContext _context;
        public RoleManagerRepository(MedicareDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            var data = await _context.Roles.ToListAsync();
            return data;
        }
    }
}
