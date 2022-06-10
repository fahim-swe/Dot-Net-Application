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

        public async Task<List<UserPost>> getAllUserPosts()
        {
            var userPost =await _context.UserPosts.Include(x => x.User).
                OrderByDescending(x => x.CreatedDate).
                ToListAsync();
            return userPost;
        }

        public Task<UserPost> getPostById(Guid id)
        {
            var userPost = _context.UserPosts.Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            return userPost;
        }
    }
}