
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
       public static async Task SeedUsers(DataContext context)
       {
        //    if(await context.Users.AnyAsync()) return;

           var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
           var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
        
           foreach(var user in users){
               using var hmac = new HMACSHA512();
               user.UserName = user.UserName.ToLower();
               user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("12345678"));
               user.passwordSalt = hmac.Key;

               AppUser  _user = user;
               context.Add(_user);
           }

           await context.SaveChangesAsync();

        }
    }
}
