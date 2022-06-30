using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Message
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        public Guid SenderId {get; set;}

        public string SerderUsername {get; set;}

        public AppUser Sender {get; set;}

        public Guid RecipientId {get; set;}

        public string RecipientUsername {get; set;}

        public AppUser Recipient {get; set;}

        public string Content {get; set;}

        public DateTime? DateRead {get; set;} 

        public DateTime MessageSent {get; set;} = DateTime.Now;

        public bool SenderDeleted {get; set; }
        public bool RecipientDeleted {get; set;}

    }
}