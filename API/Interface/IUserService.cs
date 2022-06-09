using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interface
{
    public interface IUserService
    {
        Task<List<AppUser>> getAllUsers();
        Task<AppUser> getUserById(Guid id);
    }
}