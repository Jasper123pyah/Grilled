using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore;
using Grilled.Models;
using Grilled.Data;
using Microsoft.AspNetCore.Hosting;

namespace Grilled.Controllers
{
    public class ProductController : Controller
    {
        ProductModel product = new ProductModel();
        AccountModel account = new AccountModel();
        private readonly GrilledContext context;
        private IWebHostEnvironment environment;

        public ProductController(GrilledContext _context, IWebHostEnvironment _environment)
        {
            context = _context;
            environment = _environment;
        }

        //public void AddProduct(
        //               AccountModel account,
        //               string name,
        //               string category,
        //               string designer,
        //               string size,
        //               string color,
        //               string condition,
        //               string description,
        //               string price,
        //               string shipping
                       
        //               )
        //{
        //    product = account.Products.FirstOrDefault(p => p.Name == name);

        //    if (product == null)
        //    {
        //        ProductModel newproduct = new ProductModel()
        //        {
        //            Name = name,
        //            Category = category,
        //            Designer = designer,
        //            Size = size,
        //            Color = color,
        //            Condition = condition,
        //            Description = description,
        //            Price = price,
        //            Shipping = shipping

        //        };
        //        account.Products.Add(newproduct);
        //        context.SaveChanges();
        //    }
        //}


        // GET: ProductController
        public ActionResult Sell()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sell(ProductModel product, List<IFormFile> postedFiles)
        {
            context.Database.EnsureCreated();

            account = context.Account.FirstOrDefault(a => a.Username == "Jasper");
            string wwwPath = this.environment.WebRootPath;
            string contentPath = this.environment.ContentRootPath;

            string path = Path.Combine(this.environment.WebRootPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
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
                    Images = uploadedFiles
                }) ;
                context.SaveChanges();
                return RedirectToAction("Items", "Account");
            }
            else
            {
                return View(new ProductModel());
            }
        }
    }
}
