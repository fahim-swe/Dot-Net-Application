using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entity;
using API.Helper;

namespace API.Interface
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser> GetUserByIdAsync(Guid Id);
        Task<AppUser> GetUserByUsernameAsync(string username);

        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberAsync(string username);
    }
}