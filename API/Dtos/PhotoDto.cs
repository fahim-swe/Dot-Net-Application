using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PhotoDto
    {
        public string Id {get; set;}
        public string Url {get; set;}
        public bool IsMain {get; set;}
    }
}