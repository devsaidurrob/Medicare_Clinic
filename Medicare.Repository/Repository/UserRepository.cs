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
    public class UserRepository : IUserRepository
    {
        private readonly MedicareDbContext _context;

        public UserRepository(MedicareDbContext context)
        {
            _context = context;
        }

        public async Task<PagingResult<User>> GetAllAsync(UserFilter filter)
        {
            var query = _context.Users.AsQueryable();

            // Apply filters
            if (filter.Active != null)
                query = query.Where(d => d.IsActive == filter.Active);

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

            return new PagingResult<User>
            {
                Total = total,
                Records = records
            };
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            //await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.Deleted = true;
                int retVal = await _context.SaveChangesAsync();
                return retVal > 0;
            }
            return false;
        }

        public async Task<User?> FindUserByAsync(string identifier)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u =>
                    u.UserName == identifier ||
                    u.Email == identifier ||
                    u.PhoneNumber == identifier);
        }
    }
}
