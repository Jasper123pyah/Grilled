using System;

namespace GrilledCommon.Models
{
    public class ForYouModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int ClickCount { get; set; }
    }
}