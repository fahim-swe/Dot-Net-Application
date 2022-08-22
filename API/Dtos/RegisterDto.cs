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

        [Required]
        [StringLength(8), MinLength(4)]
        public string Password {get; set;}
    }
}