using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using E_Commerce_App_Practices_1.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    public class AccountService: EntityBaseRepository<AppUser>, IAccountService
    {
        readonly DataContext _context;
        public AccountService(DataContext context): base(context){
            _context = context;
        }

        public async Task<AppUser> getByUserName(string Username)
        {
            return  await _context.Users.Where(x => (
                x.UserName == Username
            )).FirstOrDefaultAsync();

            // retn await _context.Users.FirstOrDefaultAsync(_ => _.UserName == Username);
        }

        public async Task<bool> isAnyUserExit(string Username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == Username);
        }
    }
}