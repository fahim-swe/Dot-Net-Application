
using API.Data;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using API.Helper;

namespace API.Service
{
    public class UserRepository : IUserRepository
    {
        private const bool V = false;
        private readonly DataContext _context;
    
        public UserRepository(DataContext context){
            _context = context;
        }

        public async Task<int> CountAsync() => await _context.Users.CountAsync();

        public async Task<string> DeletePhoto(Guid id, string userId)
        {
            Guid appUserId = Guid.Empty;
            appUserId = Guid.Parse(userId);

            var result = await _context.Photos.Where(x=>(
                x.Id == id & 
                x.AppUserId == appUserId
            )).FirstOrDefaultAsync();

           
            if(result == null || result.IsMain == true ) return null;
            
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

    

        public async Task<List<AppUser>> GetUsersAsync(PaginationFilter filter)
        {
            var minAge = DateTime.Today.AddYears(-filter.minAge);
            var maxAge = DateTime.Today.AddYears(-filter.maxAge - 1);
            string userName = filter.getName();

            var _user = (from u in _context.Users
                         join p in _context.Photos on u.Id equals p.AppUserId into eGroup
                         from p in eGroup.DefaultIfEmpty()
                         select new {
                            u.Id,
                            u.UserName,
                            u.DateOfBirth,
                            u.KnownAs,
                            u.Created,
                            u.LastActive,
                            u.Gender,
                            u.Introduction,
                            u.LookingFor,
                            u.Interests,
                            u.City,
                            u.Country,
                            u.Photos
                         });

            _user = _user.Where(x=> x.UserName != filter.getName());
            _user = _user.Where( x => x.DateOfBirth <= minAge && x.DateOfBirth >= maxAge);

            if(filter.Gender == 1) _user = _user.Where(x => x.Gender == "Male");
            else if(filter.Gender == 2) _user = _user.Where(x => x.Gender == "Female");

            if(filter.OrderedBy == 1) _user = _user.OrderByDescending(x => x.Created);
            else if(filter.OrderedBy == 2) _user = _user.OrderByDescending(x => x.LastActive);

            _user = _user.Skip((filter.PageNumber - 1) * filter.PageSize)
                   .Take(filter.PageSize);

           
            
            var user = await _user.ToListAsync();

            var jsonString = JsonConvert.SerializeObject(user);

            var result = JsonConvert.DeserializeObject<List<AppUser>>(jsonString);
           
            
           return result;

            //  var _user =  (from u in _context.Users
            //              join p in _context.Photos on u.Id equals p.AppUserId into eGroup
            //              from p in eGroup.DefaultIfEmpty()
            //              select new {
            //                 u.Id,
            //                 u.UserName,
            //                 u.DateOfBirth,
            //                 u.KnownAs,
            //                 u.Created,
            //                 u.LastActive,
            //                 u.Gender,
            //                 u.Introduction,
            //                 u.LookingFor,
            //                 u.Interests,
            //                 u.City,
            //                 u.Country,
            //                 u.Photos
            //              }).AsQueryable();


            // _user = _user.Where(
            //                 x=> x.Gender == "Male" && filter.Gender == 1
            //              )
            //              .Where(
            //                 x=> x.Gender == "Female" && filter.Gender == 2
            //              )
            //              .Where(
            //                 x=> x.Created >= minAge && x.Created <= maxAge
            //              )
            //              .Skip((filter.PageNumber - 1) * filter.PageSize)
            //              .Take(filter.PageSize).AsQueryable();
            
            // var _result = await _user.ToListAsync();
             
            // Select by age range 

            // Order by created

            // Order by last active

            // Select male or female
            


            // var setting = new Newtonsoft.Json.JsonSerializerSettings();
            // setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

          

            // var urs = await _context.Users.ToListAsync();
            
            // var users = await _context.Users 
            //             .Include(x => x.Photos)
            //             .Skip(3)
            //             .Take(3)
            //             .ToListAsync();
            // return users;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetMainPhoto(Guid id, Guid userId)
        {
            var photo = await _context.Photos.Where(item => (
                (item.Id == id  && item.AppUserId == userId) || 
                (item.IsMain == true &&   item.AppUserId == userId)
              
            )).Take(2).ToListAsync();
            if(photo.Count() == 0) return false;

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