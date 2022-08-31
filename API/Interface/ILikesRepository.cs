using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entity;

namespace API.Interface
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(Guid sourceUserId, Guid likedUserId);
        Task<AppUser> GetUserWithLiked(Guid userId);
        Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, Guid userId);
        
    }
}