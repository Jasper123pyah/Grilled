using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grilled.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public string Designer { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Shipping { get; set; }
        public List<string> Images { get; set; }

    }
}
