using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace API.SignalR
{
    public class MessageHub: Hub
    {
        private readonly IMessageService _meesageService;   
        private readonly IUserRepository _userService;
        private readonly IMapper _mapper;

        public MessageHub(IMessageService messageService, IMapper mapper, IUserRepository userService)
        {
            _meesageService = messageService;
            _mapper = mapper;
            _userService = userService;

        }


        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext?.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUserName(), otherUser);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var messages = await _meesageService.GetMessagesThread(Context.User.GetUserName(), otherUser);

            await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessage(CreateMessageDTO createMessageDTO)
        {
            var username = Context.User.GetUserName();
            
            if(username == createMessageDTO.RecipientUsername) 
                throw new HubException("You cannot send messages to yourself");
              
            var _sender = await _userService.GetUserByUserNameAsync(username);
            var _recipient = await _userService.GetUserByUserNameAsync(createMessageDTO.RecipientUsername);

            var jsonStringSender = JsonConvert.SerializeObject(_sender);

            var jsonStringReciver = JsonConvert.SerializeObject(_recipient);

            var sender = JsonConvert.DeserializeObject<AppUser>(jsonStringSender);
            
            var recipient = JsonConvert.DeserializeObject<AppUser>(jsonStringReciver);

            if(recipient == null) throw new HubException("Not found user");

            var message = new Message{
               SenderId = sender.Id,
               SerderUsername = sender.UserName,
    
               RecipientId = recipient.Id,
               RecipientUsername = recipient.UserName,

               Content = createMessageDTO.Content
            };

            await _meesageService.AddMessage(message);

            var group = GetGroupName(sender.UserName, recipient.UserName);
            await Clients.Group(group).SendAsync("NewMessage", message);
        }

        
        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}