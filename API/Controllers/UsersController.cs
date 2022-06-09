using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Helper;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _context;

        public UsersController(IUserService context){
            _context = context;
        }
        
        [HttpGet]
        public async Task<List<AppUser>> GetUsers()
        {
            var users = await _context.getAllUsers();
            return users;
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> getUserById(Guid id)
        {
            var user = await _context.getUserById(id);
            return Ok(new Response<AppUser>(user));
        }
            
    }
}