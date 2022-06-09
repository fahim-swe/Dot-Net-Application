using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interface;
using E_Commerce_App_Practices_1.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    public class PostService : EntityBaseRepository<UserPost>, IPostService
    {
        private readonly DataContext _context;
        public PostService(DataContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<AppUser>> getAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}