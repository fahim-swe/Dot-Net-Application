using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Service
{
    public class UserRepository : IUserRepository
    {
        private const bool V = false;
        private readonly DataContext _context;
    
        public UserRepository(DataContext context){
            _context = context;
        }

        public async Task<string> DeletePhoto(Guid id)
        {
            
            var result = await _context.Photos.FirstOrDefaultAsync(x =>(
               x.Id == id
            ));

            if(result == null || result.IsMain == true) return null;
            

            EntityEntry entityEntry = _context.Entry<Photos>(result);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return result.PublicId;
        }

        public async Task<object> GetUserByIdAsync(Guid id)
        {
            
            var user = await _context.Users 
                        .Select(c => new {
                            c.Id,
                            c.UserName,
                            c.City,
                            c.Country,
                            c.Created,
                            c.Gender,
                            c.Interests,
                            c.Introduction,
                            c.KnownAs,
                            c.LastActive,
                            c.LookingFor,
                            c.DateOfBirth,
                            photos = c   
                                .Photos
                                .Select( e => new {e.Id, e.Url, e.IsMain})
                                .ToList()
                        })
                        .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<object> GetUserByUserNameAsync(String UserName)
        {
            var user = await _context.Users 
                        .Select(c => new {
                            c.Id,
                            c.UserName,
                            c.City,
                            c.Country,
                            c.Created,
                            c.Gender,
                            c.Interests,
                            c.Introduction,
                            c.KnownAs,
                            c.LastActive,
                            c.LookingFor,
                            c.DateOfBirth,
                            c.passwordHash,
                            c.passwordSalt,
                            photos = c   
                                .Photos
                                .Select( e => new {e.Id, e.Url, e.IsMain})
                                .ToList()
                        })
                        .FirstOrDefaultAsync(x => x.UserName == UserName);
            return user;
        }

    

        public async Task<object> GetUsersAsync()
        {
            var urs = await _context.Users.ToListAsync();

            var users = await _context.Users 
                        .Select(c => new {
                            c.Id,
                            c.UserName,
                            c.City,
                            c.Country,
                            c.Created,
                            c.Gender,
                            c.Interests,
                            c.Introduction,
                            c.KnownAs,
                            c.LastActive,
                            c.LookingFor,
                            c.DateOfBirth,
                            photos = c   
                                .Photos
                                .Select( e => new {e.Id, e.Url, e.IsMain})
                                .ToList()
                        }).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetMainPhoto(Guid id)
        {
            var photo = await _context.Photos.Where(item => (
                item.Id == id  || 
                item.IsMain == true
            )).Take(2).ToListAsync();
            if(photo==null) return false;

            foreach(var item in photo){
                if(item.Id == id) item.IsMain = true;
                else item.IsMain = false;  

                EntityEntry entityEntry = _context.Entry<Photos>(item);
                entityEntry.State = EntityState.Modified;
                await _context.SaveChangesAsync();             
            }

            return true;
        }

        public async Task Update(AppUser user)
        {
            EntityEntry entityEntry = _context.Entry<AppUser>(user);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UploadPhoto(Photos photo)
        {
            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();
        }


    }
}