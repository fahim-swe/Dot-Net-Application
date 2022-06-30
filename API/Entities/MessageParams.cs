using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helper;

namespace API.Entities
{
    public class MessageParams : PaginationFilter
    {
        public string Username {get; set;} = "NONE";
        public string Container {get; set;} = "Unread";
    }
}