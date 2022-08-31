using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Entity;
using API.Extensions;
using API.Helper;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            var username = User.GetUsername();
            var user = await _userRepository.GetUserByUsernameAsync(username);
            userParams.CurrentUsername = user.UserName;

            if(string.IsNullOrEmpty(userParams.Gender)){
                userParams.Gender = user.Gender == "male" ? "female" : "male";
            }
            var users = await _userRepository.GetMembersAsync(userParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);
            return Ok(user);
        }
    }
}