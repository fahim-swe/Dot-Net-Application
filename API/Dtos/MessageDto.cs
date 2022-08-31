using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class MessageDto
    {
        
        public Guid Id {get; set;}
        public Guid SenderId {get; set;}
        public string SenderUsername {get; set;}
        public string SenderPhotoUrl {get; set;}

        public string RecipientId {get; set;}
        public string RecipientUsername {set; get;}
        public string RecipientPhotoUrl {get; set;}
        

        public string Content {get; set;}
        public DateTime? MessageSent {get; set;}
        public DateTime? DateRead {get; set;}
    }
}