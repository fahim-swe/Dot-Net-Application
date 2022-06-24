using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
         public static string GetUserName(this ClaimsPrincipal user)
         {
             return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
         }

         public static string GetUserId(this ClaimsPrincipal user)
         {
            return user.FindFirst(ClaimTypes.Authentication)?.Value;
         }
    }
}