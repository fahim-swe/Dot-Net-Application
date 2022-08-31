using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entity;
using API.Extensions;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {

        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<UserLike> GetUserLike(Guid sourceUserId, Guid likedUserId)
        {
            var userLike = await _context.Likes.FirstOrDefaultAsync( x => x.SourceUserId == sourceUserId 
                && x.LikedUserId == likedUserId
            );

            return userLike;
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, Guid userId)
        {
            var users = _context.Users.OrderBy( u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if(predicate == "liked"){
                likes = likes.Where( likes => likes.SourceUserId == userId);
                users = likes.Select(like => like.LikedUser);
            }

            if(predicate == "likedBy")
            {
                likes = likes.Where( like => like.LikedUserId == userId);
                users = likes.Select( like => like.SourceUser);
            }


            return await users.Select( user => new LikeDto{

                Username = user.UserName,
                KnownAs  = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault( p => p.IsMain).Url,
                City = user.City,
                Id = user.Id

            }).ToListAsync();

        }

        public async Task<AppUser> GetUserWithLiked(Guid userId)
        {
            
            var user =  await _context.Users
                .Include( x => x.LikedUsers)
                .FirstOrDefaultAsync( x => x.Id == userId);
            return user;
        }
    }
}