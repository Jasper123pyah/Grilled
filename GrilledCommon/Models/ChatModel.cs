﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrilledCommon.Models
{
    public class ChatModel
    {
        public Guid Id { get; set; }
        public string Users { get; set; }
        public ProductModel Product { get; set; }
        public List<MessageModel> Messages { get; set; }
    }
}
