using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interface
{
    public interface IUserRepository
    {
        Task Update(AppUser user); 
        Task<bool> SaveAllAsync();

        Task<object> GetUsersAsync();

        Task<object> GetUserByIdAsync(Guid id);

        Task<object> GetUserByUserNameAsync(String UserName);

        Task UploadPhoto(Photos photo);
        Task<string> DeletePhoto(Guid photoId, string userId);

        Task<bool> SetMainPhoto(Guid id, Guid userId);
        
    }
}