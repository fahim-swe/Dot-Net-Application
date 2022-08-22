using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entity;

namespace API.Dtos
{
    public class MemberDto
    {
        public string Id {get; set;}
        public string UserName {get; set;}
        public string KnownAs {get; set;}
        public string ProfileUrl {get; set;}
        public DateTime Created {get; set;}
        public DateTime LastActive {get; set;}
        public string Gender {get; set;}
        public int Age{get; set;}
        public string Introduction {get; set;}
        public string LookingFor {get; set;}
        public string Interests {get; set;}
        public string City {get; set;}
        public string Country {get; set;}
        public ICollection<Photo>? Photos {get; set;}
    }
}