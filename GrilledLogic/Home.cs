using GrilledCommon.Models;
using GrilledData.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrilledLogic
{
    public class Home
    {
        DisplayProductModel display = new DisplayProductModel();
        CommonFunctions functions = new CommonFunctions();

        public DisplayProductModel IndexDisplay(HttpContext httpContext, GrilledContext context)
        {
            display.Products = new List<ProductModel>();

            foreach (ProductModel product in context.Product)
            {
                functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
            }
            return display;
        }
        public DisplayProductModel Search(string search, string category, HttpContext httpContext, GrilledContext context)
        {
            display.Products = new List<ProductModel>();

            if (category == "All Categories")
            {
                if (search == null)
                {
                    foreach (ProductModel product in context.Product)
                    {
                        functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
                    }
                }
                else
                {
                    foreach (ProductModel product in context.Product)
                    {
                        if (product.Name.ToLower().Contains(search.ToLower()) || product.Designer.ToLower().Contains(search.ToLower()) || product.Description.ToLower().Contains(search.ToLower()))
                        {
                            functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
                        }
                    }
                }
            }
            else
            {
                foreach (ProductModel product in context.Product)
                {
                    if (product.Category == category)
                    {
                        if (search == null)
                        {
                            functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
                        }
                        else if (product.Name.ToLower().Contains(search.ToLower()) || product.Designer.ToLower().Contains(search.ToLower()) || product.Description.ToLower().Contains(search.ToLower()))
                        {
                            functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
                        }
                    }
                }
            }
            return display;
        }
    }
}
