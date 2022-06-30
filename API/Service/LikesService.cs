using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    public class LikesService : ILikesService
    {
        private readonly DataContext _context;

        public LikesService(DataContext context)
        {
            _context = context;
        }
        public async Task<UserLike> GetUserLike(Guid sourceUserId, Guid likedUserId)
        {
            return await _context.Likes.Where(x => x.SourceUserId == sourceUserId && 
                                                x.LikedUserId == likedUserId).FirstOrDefaultAsync();

        }

        public Task<IEnumerable<LikeDTO>> GetUserLikes(string predicate, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserWithLikes(Guid userId)
        {
            return await _context.Users.Include(x => x.LikedByUsers).FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}