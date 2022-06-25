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
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
     
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _service;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        
        public AccountController(IAccountService service, ITokenService tokenService, IMapper mapper){
            _service = service;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> CreaterAccount(RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid) return BadRequest(new Response<String>("Bad Formate"));

            if(await _service.isAnyUserExit(registerDTO.Username)) return BadRequest("User-name already exit");
            
            var user = _mapper.Map<AppUser>(registerDTO);

            using var hmac = new HMACSHA512();
            user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
            user.passwordSalt  = hmac.Key;

            await _service.AddAsync(user);

            var userDto = new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs
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
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs
            };
            
            return Ok(new Response<UserDto> (userDto));
        }
    }
}