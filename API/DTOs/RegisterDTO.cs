using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDTO
    {
        [Required] public string Username {get; set;}
          
        [Required] public String KnownAs { get; set; }

        [Required] public String Gender { get; set; }

        [Required] public DateTime DateOfBirth { get; set; }

        [Required] public String City { get; set; }

        [Required] public String Country { get; set; }


        [Required]
        [StringLength(8, MinimumLength =4)] 
        public string Password {get; set;}       
    }
}