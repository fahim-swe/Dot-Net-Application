using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Message
    {
        [Key]
        public Guid Id {get; set;}
        public Guid SenderId {get; set;}
        public string SenderUsername {get; set;}
        public AppUser Sender {get; set;}

        public Guid RecipientId {get; set;}
        public string RecipientUsername {set; get;}
        public AppUser Recipient {get; set;}
        

        public string Content {get; set;}


        public DateTime? MessageSent {get; set;} = DateTime.Now;
        public DateTime? DateRead {get; set;}
        public bool? SenderDeleted {get; set;}
        public bool? RecipientDeleted {get; set;}

    }
}