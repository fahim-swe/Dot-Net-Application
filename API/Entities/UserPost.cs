using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using API.Enums;
using E_Commerce_App_Practices_1.Data.Base;

namespace API.Entities
{
    public class UserPost : IEntityBase
    {
        [Key()]
        public Guid Id {get; set;}
        public DateTime CreatedDate {get; set;}
        public DateTime LastModify {get; set;}

        public PostType Type {get; set;}

        public String Message {get; set;}

        public Guid UserId {get; set;}
        [ForeignKey("UserId")]
        
        public AppUser User {get; set;}
    }
}