using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helper;
using API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
     
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _service;
        private readonly ITokenService _tokenService;
        
        public AccountController(IAccountService service, ITokenService tokenService){
            _service = service;
            _tokenService = tokenService;
        }


        [HttpPost]
        public async Task<IActionResult> CreaterAccount(RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid) return BadRequest(new Response<String>("Bad Formate"));

            var user = await _service.getByUserName(registerDTO.UserName);
            if(user != null) return BadRequest(new Response<String>("Exit User"));

            using var hmac = new HMACSHA512();
            var User = new AppUser{
                UserName = registerDTO.UserName.ToLower(), 
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                passwordSalt = hmac.Key
            };

            await _service.AddAsync(User);

            var userDto = new UserDto{
                Username = User.UserName,
                Token = _tokenService.CreateToken(User)
            };
            
            return Ok(new Response<UserDto>(userDto));
        }
        

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if(!ModelState.IsValid) return Ok(new Response<String>("Bad Formate"));

            var user = await _service.getByUserName(loginDTO.UserName);
            if(user == null) return Unauthorized(new Response<String> ("Invalid Username"));

            using var hmac = new HMACSHA512(user.passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for(int i = 0; i < computedHash.Length; i++){
                if(computedHash[i] != user.passwordHash[i]){
                    return Unauthorized(new Response<String>("Wrong Password"));
                }
            }

            var userDto = new UserDto{
                Username =user.UserName,
                Token = _tokenService.CreateToken(user)
            };
            
            return Ok(new Response<UserDto> (userDto));
        }
    }
}