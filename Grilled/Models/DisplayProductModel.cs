using Grilled.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grilled.Models
{
    public class DisplayProductModel
    {
        public List<ProductModel> Products { get; set; }

        public void AddToDisplay(ProductModel product, GrilledContext context, string imagepath)
        {
            ProductModel productAdd = context.Product.Where(p => p.Id == product.Id).Include(a => a.Images).FirstOrDefault();
            productAdd.Images[0].Source = imagepath + productAdd.Images[0].Name;
            this.Products.Add(productAdd);
        }
    }
}
