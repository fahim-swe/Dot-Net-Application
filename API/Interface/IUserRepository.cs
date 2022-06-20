using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interface
{
    public interface IUserRepository
    {
        void Update(AppUser user); 
        Task<bool> SaveAllAsync();

        Task<object> GetUsersAsync();

        Task<object> GetUserByIdAsync(Guid id);

        Task<object> GetUserByUserNameAsync(String UserName);
    }
}