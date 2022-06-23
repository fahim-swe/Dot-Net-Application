using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
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
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService){
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;

        }   
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = (await _userRepository.GetUsersAsync());

            var jsonString = JsonConvert.SerializeObject(users);

            var result = JsonConvert.DeserializeObject<List<AppUser>>(jsonString);
            
            return Ok(_mapper.Map<List<MemberDto>>(result));
        }


        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByName(string username)
        {
           

     
            var user = await _userRepository.GetUserByUserNameAsync(username);
           
            var jsonString = JsonConvert.SerializeObject(user);
            
            var result = JsonConvert.DeserializeObject<AppUser>(jsonString);
            
            
            return Ok(_mapper.Map<MemberDto> (result));
        }   


        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.GetUserName();
            
            var _object = await _userRepository.GetUserByUserNameAsync(username);
           
            var jsonString = JsonConvert.SerializeObject(_object);
            
            var user = JsonConvert.DeserializeObject<AppUser>(jsonString);
            Console.WriteLine(user);

            user = _mapper.Map(memberUpdateDto, user);
         

            await _userRepository.Update(user);

            return NoContent();
            
        }    

        [HttpPost("add-photo")]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            var username = User.GetUserName();
                   
            var _object = await _userRepository.GetUserByUserNameAsync(username);
           
            var jsonString = JsonConvert.SerializeObject(_object);
            
            var user = JsonConvert.DeserializeObject<AppUser>(jsonString);

            var result = await _photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photos{
                Url = result.SecureUri.AbsoluteUri,
                PublicId = result.PublicId,
                AppUserId = user.Id,
            };
            
            if(user.Photos.Count == 0){
                photo.IsMain = true;
            }

            await _userRepository.UploadPhoto(photo);
            
            return Ok(new Response<PhotoDto>((_mapper.Map<PhotoDto>(photo))));
            
        } 


        [HttpDelete("delete-photo/{photoId}")]
        public async Task<IActionResult> DelelePhoto(Guid photoId)
        {
            var publicUri = await _userRepository.DeletePhoto(photoId);
            if(publicUri != null){
                await _photoService.DelelePhotoAsync(publicUri);
                return Ok("Deleted");
            }
            return BadRequest("Photo doesn't exits or it is your main photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(Guid photoId)
        {
            return Ok(await _userRepository.SetMainPhoto(photoId));
        }
    }
}