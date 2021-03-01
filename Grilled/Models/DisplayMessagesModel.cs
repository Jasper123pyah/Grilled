using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grilled.Models
{
    public class DisplayMessagesModel
    {
        public List<MessageModel> Messages { get; set; }
        public List<ChatModel> Chats { get; set; }
    }
}
