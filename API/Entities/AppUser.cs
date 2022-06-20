using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using API.Helper;
using E_Commerce_App_Practices_1.Data.Base;

namespace API.Entities
{
    public class AppUser : IEntityBase
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }
        public String UserName { get; set; }

        public byte[] passwordSalt { get; set; }
        public byte[] passwordHash { get; set; }


        public DateTime DateOfBirth { get; set; }
        public String KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;

        public String Gender { get; set; }

        public String Introduction { get; set; }

        public String LookingFor { get; set; }

        public String Interests { get; set; }

        public String City { get; set; }

        public String Country { get; set; }

        public List<Photos> Photos { get; set; }

        public int GetAge()
        {
            return DateTimeHelper.CalculateAge(this.DateOfBirth);
        }
    }   
}