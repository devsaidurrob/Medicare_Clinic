using Medicare.Repository.Entity;
using Medicare.Repository.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Finds a user by username, email, or phone number.
        /// </summary>
        Task<User?> FindUserByAsync(string identifier);
        Task<PagingResult<User>> GetAllAsync(UserFilter filter);
        Task<User?> GetByIdAsync(Guid id);
        Task<User> AddAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
