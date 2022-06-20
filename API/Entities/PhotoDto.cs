using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class PhotoDto
    {
       
        public Guid Id { get; set; }
        public String Url { get; set; }

        public bool IsMain { get; set; }
    }
}