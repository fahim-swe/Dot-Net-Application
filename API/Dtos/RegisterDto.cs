using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RegisterDto
    {
        
        [Required]
        [MinLength(4)]
        public string UserName {get; set;}
        public string KnownAs {get; set;}

        [Required]
        public DateTime DateOfBirth {get; set;}
    
        public DateTime Created {get; protected set;} = DateTime.Now;

        [Required]
        public string Gender {get; set;}

        [Required]
        public string City {get; set;}

        [Required]
        public string Country {get; set;}

        [Required]
        [StringLength(8), MinLength(4)]
        public string Password {get; set;}
    }
}