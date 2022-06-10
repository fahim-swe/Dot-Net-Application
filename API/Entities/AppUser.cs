using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_App_Practices_1.Data.Base;

namespace API.Entities
{
    public class AppUser: IEntityBase
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id {get; set;}
        public string UserName {get; set;}

        public byte[] passwordSalt {get; set;}
        public byte[] passwordHash {get; set;}
    
    }
}