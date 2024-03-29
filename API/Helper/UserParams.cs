using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class UserParams : PaginationParams
    {
        public string? CurrentUsername {get; set;}
        public string? Gender {get; set;}

        public int MinAge {get; set;} = 15;
        public int MaxAge {get; set;} = 110;
        public string OrderBy {get; set;} = "lastActive";
    }
}