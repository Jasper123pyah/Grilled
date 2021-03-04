using GrilledCommon.Models;
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
        AccountModel account = new AccountModel();
        CommonFunctions functions = new CommonFunctions();

        public ProductModel Change(ProductModel product, GrilledContext context)
        {
            ProductModel model = context.Product.FirstOrDefault(p => p.Id == product.Id);
            account = context.Account.FirstOrDefault(a => a.Username == model.OwnerName);

            model.Name = product.Name;
            model.Condition = product.Condition;
            model.Description = product.Description;
            model.Price = product.Price;
            model.Shipping = product.Shipping;

            context.SaveChanges();
            return product;
        }
        public bool Sell(ProductModel product, List<IFormFile> postedFiles, string _path , HttpContext httpContext, GrilledContext context)
        {
            account = context.Account.FirstOrDefault(a => a.Username == httpContext.Request.Cookies["Login"]);

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

            if (account != null)
            {
                if (account.Products == null)
                    account.Products = new List<ProductModel>();

                account.Products.Add(new ProductModel()
                {
                    Name = product.Name,
                    Category = product.Category,
                    Designer = product.Designer,
                    Size = product.Size,
                    Color = product.Color,
                    Condition = product.Condition,
                    Description = product.Description,
                    Price = product.Price,
                    Shipping = product.Shipping,
                    Images = uploadedFiles,
                    OwnerName = account.Username
                });
                context.SaveChanges();

                return true;
            }
            else
            {
                return false; //View(new ProductModel());
            }
        }

        public ProductModel Edit(ProductModel product, HttpContext httpContext, GrilledContext context)
        {
            ProductModel model = context.Product.Include(a => a.Images).FirstOrDefault(p => p.Id == product.Id);
            model.Images[0].Source = functions.GetImagePath(httpContext) + model.Images[0].Name;
            return model;
        }
        public ProductModel Details(ProductModel product, HttpContext httpContext, GrilledContext context)
        {
            ProductModel model = context.Product.Include(a => a.Images).FirstOrDefault(p => p.Id == product.Id);
            model.Images[0].Source = functions.GetImagePath(httpContext) + model.Images[0].Name;
            return model;
        }
        public void Delete(ProductModel product, GrilledContext context)
        {
            ProductModel model = context.Product.Include(a => a.Images).FirstOrDefault(p => p.Id == product.Id);
            context.Product.Remove(model);
            context.SaveChanges();
        }
    }
}
