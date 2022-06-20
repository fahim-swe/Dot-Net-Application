using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Helper;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;



namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;       

        public UsersController(IUserRepository userRepository, IMapper mapper){
            _userRepository = userRepository;
            _mapper = mapper;
        }   
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = (await _userRepository.GetUsersAsync());

            var jsonString = JsonConvert.SerializeObject(users);

            var result = JsonConvert.DeserializeObject<List<AppUser>>(jsonString);
            
            return Ok(_mapper.Map<List<MemberDto>>(result));
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<MemberDto>> GetUserById(Guid id)
        // {
        //     var user = await _userRepository.GetUserByIdAsync(id);
        //     var jsonString = JsonConvert.SerializeObject(user);
            
        //     var result = JsonConvert.DeserializeObject<AppUser>(jsonString);

            
        //     return Ok(_mapper.Map<MemberDto> (result));
        // }     

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByName(string username)
        {
            var user = await _userRepository.GetUserByUserNameAsync(username);
           
            var jsonString = JsonConvert.SerializeObject(user);
            
            var result = JsonConvert.DeserializeObject<AppUser>(jsonString);
            
            
            return Ok(_mapper.Map<MemberDto> (result));
        }         
    }
}