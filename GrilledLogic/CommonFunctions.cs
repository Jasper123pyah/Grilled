using GrilledCommon.Models;
using GrilledData;
using GrilledData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrilledLogic
{
    public class CommonFunctions
    {
        ProductData productData = new ProductData();
        public string GetImagePath(HttpContext httpContext)
        {
            return @"https://" + httpContext.Request.Host.ToString() + @"/Uploads/";
        }
        public void AddToDisplay(ProductModel product, GrilledContext context, string imagepath, List<ProductModel> products)
        {
            ProductModel productAdd = productData.GetProduct(product.Id, context);
            productAdd.Images[0].Source = imagepath + productAdd.Images[0].Name;
            products.Add(productAdd);
        }
    }
}
