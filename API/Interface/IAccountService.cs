using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using E_Commerce_App_Practices_1.Data.Base;

namespace API.Interface
{
    public interface IAccountService : IEntityBaseRepository<AppUser>{
        Task<AppUser> getByUserName(string UserName);
    }
}