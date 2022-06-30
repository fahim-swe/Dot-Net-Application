using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interface
{
    public interface ILikesService
    {
        Task<UserLike> GetUserLike(Guid sourceUserId, Guid likedUserId);

        Task<AppUser> GetUserWithLikes(Guid userId);

        Task<IEnumerable<LikeDTO>> GetUserLikes (string predicate, Guid userId);
        
    }
}