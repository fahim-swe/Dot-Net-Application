using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class PagedList<T> : List<T>
    {
        
        public int CurrentPage {get; set;}
        public int TotalPages {get; set;}

        public int PageSize {get; set;}
        public int TotalCount {get; set;}
    }
}