using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Photos
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }
        public String Url { get; set; }

        public bool IsMain { get; set; }

        
        public String PublicId { get; set; }


        [ForeignKey("AppUserId")]
        public Guid AppUserId { get; set; }

    }
}