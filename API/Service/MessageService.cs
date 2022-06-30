using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace API.Service
{
    public class MessageService : IMessageService
    {

        private readonly DataContext _context;
        public MessageService(DataContext context){
            _context = context;
        }

        public async Task AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public Task DeleteMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public async Task<Message> GetMessage(Guid Id)
        {
            return await _context.Messages.Where(x => Id == Id).FirstOrDefaultAsync();
        }

        public Task<List<Message>> GetMessagesForUser()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages.OrderByDescending(m => m.MessageSent).AsQueryable();

            query = messageParams.Container switch{
                "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username),
                "Outbox" => query.Where(u => u.SerderUsername == messageParams.Username),

                _ => query.Where(u => u.RecipientUsername == messageParams.Username 
                        && u.DateRead == null)
            };

            query = query.Skip((messageParams.PageNumber -1)*messageParams.PageSize).Take(messageParams.PageSize);

           
            
            var messageList = await query.ToListAsync();
            

            var jsonString = JsonConvert.SerializeObject(messageList);

            var result = JsonConvert.DeserializeObject<List<Message>>(jsonString);
           
            
           return result;
        }

        public async Task<List<Message>> GetMessagesThread(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                    .Where(u => u.RecipientUsername == currentUsername && u.SerderUsername == recipientUsername
                             || u.RecipientUsername == recipientUsername && u.SerderUsername == currentUsername
                             )
                             .OrderByDescending(m => m.MessageSent)
                             .Take(10)
                             .ToListAsync();


            
            var unreadMessages = messages.Where( m=> m.DateRead == null
                                 && m.RecipientUsername == currentUsername).ToList();
            if(unreadMessages.Any())
            {
                foreach(var unreadSMS in unreadMessages){
                    unreadSMS.DateRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }

            return messages;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}