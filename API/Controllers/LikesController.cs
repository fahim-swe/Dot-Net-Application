using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entity;
using API.Extensions;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikesRepository _likesRepository;

        public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
        {
            _userRepository = userRepository;
            _likesRepository = likesRepository;
        }


        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var likedUser = await _userRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _likesRepository.GetUserWithLiked(new Guid(sourceUserId));

            if(likedUser == null) return NotFound();
            if(sourceUser.UserName == username) return BadRequest("You cannot like yourself");
            var userLike = await _likesRepository.GetUserLike(new Guid(sourceUserId), likedUser.Id);

            if(userLike != null) BadRequest("You already like this user");


            userLike = new UserLike 
            {
                SourceUserId = new Guid(sourceUserId),
                LikedUserId = likedUser.Id  
            };

            sourceUser.LikedUsers.Add(userLike);
            if(await _userRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to like user");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
        {
            var users = await _likesRepository.GetUserLikes(predicate, new Guid(User.GetUserId()));
            return Ok(users);
        } 


    }
}