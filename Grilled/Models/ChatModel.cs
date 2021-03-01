using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grilled.Models
{
    public class ChatModel
    {
        public Guid Id { get; set; }
        public string Users { get; set; }
        public Guid ProductId { get; set; }
        public List<MessageModel> Messages { get; set; }
    }
}
