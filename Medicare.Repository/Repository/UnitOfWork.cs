using Medicare.Repository.DbContext;
using Medicare.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly MedicareDbContext _context;
        public UnitOfWork(MedicareDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
