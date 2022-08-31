using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entity;
using API.Extensions;
using API.Helper;
using API.Interface;
using API.SignalR;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace API.Controllers
{

    [Authorize]
    public class MessagesController : BaseApiController
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        private readonly PresenceTracker _tracker;
      

        public MessagesController(
            IUnitOfWork unitOfWork,
            IMapper mapper, 
            IServiceProvider serviceProvider, 
            PresenceTracker tracker)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _tracker = tracker;
        }


        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();
            if(username == createMessageDto.RecipientUsername)
                return BadRequest("You cannot send messages to yourself");
            
            var sender = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var recipient = await _unitOfWork.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);
            
            if(recipient == null) return NotFound();

            var message =  new Message
            {
               SenderId = sender.Id,
               SenderUsername = sender.UserName,
               Sender = sender,

               RecipientId = recipient.Id, 
               RecipientUsername = recipient.UserName,
               Recipient = recipient,
               Content = createMessageDto.Content
            };

            if(_tracker.isOnline(message.RecipientUsername)){
                message.DateRead = DateTime.Now;
            }

            _unitOfWork.MessageRepository.AddMessage(message);
            

            if(await _unitOfWork.Complete())
            {
                var chatHub = (IHubContext<MessageHub>)_serviceProvider.GetService(typeof(IHubContext<MessageHub>));

                var group = GetGroupName(message.SenderUsername, message.RecipientUsername);
                await chatHub.Clients.Group(group).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));

                return Ok(_mapper.Map<MessageDto>(message));
            } 

            return BadRequest("Failed to send message");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]
            MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();
            var messsage = await _unitOfWork.MessageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messsage.CurrentPage, messsage.PageSize,
                messsage.TotalCount, messsage.TotalPages);
            
            return messsage;
        }


        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername();
            return Ok(await _unitOfWork.MessageRepository.GetMessageThread(currentUsername, username));
        }

        private string GetGroupName(string getUserId, string? otherUser)
        {
            var stringCompare = string.CompareOrdinal(getUserId.ToString(), otherUser) < 0;
            return stringCompare ? $"{getUserId.ToString()}-{otherUser}" : $"{otherUser}-{getUserId.ToString()}";
        }
    }
}