using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Photo
    {
        [Key]
        public Guid Id {get; set;}
        public string Url {get; set;}
        public bool IsMain {get; set;}
        public string? PublicId {get; set;}
    }
}