using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helper;

namespace API.Interface
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}