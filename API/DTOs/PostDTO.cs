using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Enums;

namespace API.DTOs
{
    public class PostDTO
    {
        public DateTime CreatedDate {get; set;}

        public PostType Type {get; set;}

        public String Message {get; set;}

        public Guid UserId {get; set;}
        
        
    }
}