using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grilled.Models
{
    public class SelectionModel
    {

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Sizes { get; set; }

        public int CategoryId { get; set; }
        public int SizeId { get; set; }
    }
}
