using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{

    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;
        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;
        }


        public override async Task OnConnectedAsync()
        {
            await _tracker.UserConnected(Context.User.GetUsername(), Context.ConnectionId);

            await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername());

            var currentUsers = await _tracker.GetOnlineUsers();
            Console.WriteLine(currentUsers.ToString());
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var isOffline =  await _tracker.UserDisconnected(Context.User.GetUsername(), Context.ConnectionId);
            
            var currentUsers = await _tracker.GetOnlineUsers();
            Console.WriteLine(currentUsers.ToString());
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);
            
            await base.OnDisconnectedAsync(exception);
        }
    }
}