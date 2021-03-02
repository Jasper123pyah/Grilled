using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrilledCommon.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public string Message {get; set;}
        public string SenderName { get; set; }
        public DateTime Time { get; set; }
    }
}
