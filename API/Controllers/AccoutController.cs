using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entity;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccoutController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccoutController(DataContext context, ITokenService tokenService, IMapper mapper){
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {

            if(!ModelState.IsValid) return BadRequest("Unvalid Data");
            if(await UserExists(register.UserName)) return BadRequest("Username already exits"); 

            
            using var hmac = new HMACSHA512();

            var user = _mapper.Map<AppUser>(register);
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
            user.PasswordSalt = hmac.Key;
            

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);

            return userDto;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            if(!ModelState.IsValid) return BadRequest("Unvalid Data");

            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == login.UserName);

            if(user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for(int i = 0; i < computedHash.Length; i++){
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Wrong Password");
            }


            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);
            
            return userDto;
        }





        private async Task<Boolean> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username);
        }

    }
}