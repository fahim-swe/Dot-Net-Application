using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IUserRepository _userService;
        private readonly IMessageService _messageService;

        public MessagesController(IUserRepository userRepository, IMessageService messageService)
        {
            _userService = userRepository;
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDTO createMessageDTO)
        {
            var username = User.GetUserName();
            if(username == createMessageDTO.RecipientUsername) 
                return BadRequest("You cannot send messages to yourself");
              
            var _sender = await _userService.GetUserByUserNameAsync(username);
            var _recipient = await _userService.GetUserByUserNameAsync(createMessageDTO.RecipientUsername);

            var jsonStringSender = JsonConvert.SerializeObject(_sender);

            var jsonStringReciver = JsonConvert.SerializeObject(_recipient);

            var sender = JsonConvert.DeserializeObject<AppUser>(jsonStringSender);
            
            var recipient = JsonConvert.DeserializeObject<AppUser>(jsonStringReciver);

            if(recipient == null) return NotFound();

            var message = new Message{
               SenderId = sender.Id,
               SerderUsername = sender.UserName,
    
               RecipientId = recipient.Id,
               RecipientUsername = recipient.UserName,

               Content = createMessageDTO.Content
            };

            await _messageService.AddMessage(message);
            return Ok(message);
        }


        [HttpGet]
        public async Task<ActionResult> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUserName();

            var result = await _messageService.GetMessagesForUser(messageParams);
            return Ok(result);
        }


        [HttpGet("thread/{username}")]
        public async Task<IActionResult> GetMessageThread(string username)
        {
            string currentUsername = User.GetUserName();

            return Ok(await _messageService.GetMessagesThread(currentUsername, username));
        }
    }
}