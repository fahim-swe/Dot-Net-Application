using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interface
{
    public interface IMessageService
    {
        Task AddMessage(Message message);

        Task DeleteMessage(Message message);

        Task<Message> GetMessage(Guid Id);

        Task<List<Message>> GetMessagesForUser( MessageParams messageParams);

        Task<List<Message>> GetMessagesThread(string currentUsername, string recipientUsername);

        Task<bool> SaveAllAsync();
         
    }
}