using GrilledCommon.Models;
using GrilledData;
using GrilledData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GrilledLogic
{
    public class Product
    {
        CommonFunctions functions = new CommonFunctions();
        ProductData productData = new ProductData();

        public ProductModel Change(ProductModel product, GrilledContext context)
        {     
            return productData.EditProduct(product, context);
        }
        public bool Sell(ProductModel product, List<IFormFile> postedFiles, string _path , HttpContext httpContext, GrilledContext context)
        {
            string path = Path.Combine(_path, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<Image> uploadedFiles = new List<Image>();

            foreach (IFormFile postedFile in postedFiles)
            {
                if (postedFile.FileName.EndsWith(".jpg") || postedFile.FileName.EndsWith(".png") || postedFile.FileName.EndsWith(".jpeg"))
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(new Image()
                        {
                            Name = fileName,
                        });
                    }
                }
                else
                    return false;
            }

            if (productData.AddProduct(context, uploadedFiles, product, httpContext.Request.Cookies["Login"]))
                return true;

           return false; 
        }

        public ProductModel Edit(ProductModel product, HttpContext httpContext, GrilledContext context)
        {
            ProductModel model = productData.GetProduct(product.Id, context); 
            model.Images[0].Source = functions.GetImagePath(httpContext) + model.Images[0].Name;
            return model;
        }
        public ProductModel Details(ProductModel product, HttpContext httpContext, GrilledContext context)
        {
            ProductModel model = productData.GetProduct(product.Id, context);
            model.Images[0].Source = functions.GetImagePath(httpContext) + model.Images[0].Name;
            return model;
        }
        public void Delete(ProductModel product, GrilledContext context)
        {
            productData.DeleteProduct(product, context);
        }
    }
}
