using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Service
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context){
            
            _context = (DataContext)context;
        }


      
        public async Task<List<AppUser>> getAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        
        public async Task<AppUser> getUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}