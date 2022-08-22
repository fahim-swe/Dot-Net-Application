using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entity;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            // return await _context.Users
            //         .Where(x => x.UserName == username)
            //         .Select( user => new MemberDto{
            //             Id = user.Id.ToString(), 
            //             UserName = user.UserName,

            //         })
            //         .SingleOrDefaultAsync();

            var member =  await _context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
            return member;
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            var members = await _context.Users
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return members;
        }

        public async Task<AppUser> GetUserByIdAsync(Guid Id)
        {
            var user =  await _context.Users.SingleOrDefaultAsync(x => x.Id == Id);
            return user;
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                    .Include(x => x.Photos)
                    .SingleOrDefaultAsync(x => x.UserName == username);
            return user;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(x => x.Photos)
                .ToArrayAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            EntityEntry entityEntry = _context.Entry<AppUser>(user);
            entityEntry.State = EntityState.Modified;
        }
    }
}