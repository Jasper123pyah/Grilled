using GrilledCommon.Models;
using GrilledData;
using GrilledData.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrilledLogic
{
    public class Home
    {
        DisplayProductModel display = new DisplayProductModel()
        {
            Products = new List<ProductModel>()
        };
        CommonFunctions functions = new CommonFunctions();
        ProductData productData = new ProductData();

        public DisplayProductModel IndexDisplay(HttpContext httpContext, GrilledContext context)
        {
            foreach (ProductModel product in productData.GetAllProducts(context))
            {
                functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
            }
            return display;
        }

        public DisplayProductModel Search(string search, string category, HttpContext httpContext, GrilledContext context)
        {
            if (category == "All Categories")
            {
                if (search == null)
                {
                    foreach (ProductModel product in productData.GetAllProducts(context))
                    {
                        functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
                    }
                }
                else
                {
                    foreach (ProductModel product in productData.GetAllProducts(context))
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
                foreach (ProductModel product in productData.GetAllProducts(context))
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
