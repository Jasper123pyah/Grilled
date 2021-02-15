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

namespace Grilled.Controllers
{
    public class ProductController : Controller
    {
        ProductModel product = new ProductModel();
        private readonly GrilledContext context;

        public void AddProduct(
                       AccountModel account,
                       string name,
                       string category,
                       string designer,
                       string size,
                       string color,
                       string condition,
                       string description,
                       string price,
                       string shipping
                       
                       )
        {
            product = account.Products.FirstOrDefault(p => p.Name == name);

            if (product == null)
            {
                ProductModel newproduct = new ProductModel()
                {
                    Name = name,
                    Category = category,
                    Designer = designer,
                    Size = size,
                    Color = color,
                    Condition = condition,
                    Description = description,
                    Price = price,
                    Shipping = shipping

                };
                account.Products.Add(newproduct);
                context.SaveChanges();
            }
        }


        // GET: ProductController
        public ActionResult Sell()
        {
            return View();
        }

    }
}
