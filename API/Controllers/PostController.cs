using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Enums;
using API.Helper;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  
    // [Authorize]
    public class PostController: BaseApiController
    {
        private readonly IPostService _service;


        public PostController(IPostService service){
            _service = service;
           
        }      

        [HttpGet]
        public async Task<List<UserPost>> GetAllPost()
        {   
           return await _service.getAllUserPosts();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var userPost = await _service.getPostById(id);
            return Ok(new Response<UserPost> (userPost));
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePost(PostDTO postDTO)
        {
            if(!ModelState.IsValid) return Ok(new Response<String>("Bad Formate"));

            var post = new UserPost{
                CreatedDate = postDTO.CreatedDate,
                LastModify = postDTO.CreatedDate,
                Message = postDTO.Message,
                Type = postDTO.Type,
                UserId = postDTO.UserId
            };
          

            await _service.AddAsync(post);
            return Ok(new Response<UserPost>(post));
        }


        [HttpPut]
        public async Task<IActionResult> UpdatePost(Guid Id, PostDTO postDTO)
        {
            return Ok(new Response<String>("Not Mofify"));
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok(new Response<String> ("Deleted Successfully"));
        }

    }
}