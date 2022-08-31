using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class UserLike
    {

        [Key]
        public AppUser SourceUser {get; set;}
        public Guid SourceUserId {get; set;}

        [Key]
        public AppUser LikedUser {get; set;}
        public Guid LikedUserId {get; set;}
    }
}