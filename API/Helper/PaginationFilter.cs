using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int minAge {get; set;} = 18;
        public int maxAge {get; set; } = 150;

        private string UserName{set; get;} 

        public int Gender {get; set;} = 0;

        public int OrderedBy {get; set;} = 0;

        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 50;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }

        public string getName(){
            return this.UserName;
        }

        public void setName(string username){
            this.UserName = username;
        }

    }
}