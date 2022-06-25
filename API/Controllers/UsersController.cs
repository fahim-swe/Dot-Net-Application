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


        public UsersController(IUserRepository userRepository, 

            IMapper mapper, IPhotoService photoService){
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
           
        }   
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            
    
            var data = _mapper.Map<List<MemberDto>>(await _userRepository.GetUsersAsync(validFilter));
        
            var totalRecords = await _userRepository.CountAsync();

            // var pagedResponse = PaginationHelper.CreatePagedReponse<MemberDto>(data, validFilter, totalRecords, _uriService, route);

            return Ok(data); 
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
            var result = await _photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);
            
           

            var photo = new Photos{
                Url = result.SecureUri.AbsoluteUri,
                PublicId = result.PublicId,
                AppUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.Authentication))
            };

            await _userRepository.UploadPhoto(photo);
            
            return Ok(new Response<PhotoDto>((_mapper.Map<PhotoDto>(photo))));
            
        } 


        [HttpDelete("delete-photo/{photoId}")]
        public async Task<IActionResult> DelelePhoto(Guid photoId)
        {
            // var userId = "";
            // foreach (var claim in User.Claims){
            // //    Console.WriteLine("Claim:{0} Value:{1}", claim.Type, claim.Value);
            // //    Console.WriteLine(claim.ToString());
            //     Console.WriteLine(claim.Type.ToString()  + " " + claim.Value.ToString());
            //     if(claim.Type.ToString() == "name"){
            //         userId = claim.Value.ToString();
            //         break;
            //     }
            // }

        
            // Console.WriteLine(userId);
            // Console.WriteLine("What: " + User.FindFirstValue(ClaimTypes.Anonymous));

            var publicUri = await _userRepository.DeletePhoto(photoId, User.FindFirstValue(ClaimTypes.Authentication).ToString());
            if(publicUri != null){
                await _photoService.DelelePhotoAsync(publicUri);
                return Ok("Deleted");
            }
            return BadRequest("Photo doesn't exits or it is your main photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(Guid photoId)
        {
            Guid appUserId = Guid.Empty;
            appUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.Authentication));

            return Ok(await _userRepository.SetMainPhoto(photoId, appUserId));
        }
    }
}