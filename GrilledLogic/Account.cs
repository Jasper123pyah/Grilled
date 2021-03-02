using System;
using System.Collections.Generic;
using System.Linq;
using GrilledCommon.Models;
using GrilledData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace GrilledLogic
{
    public class Account
    {
        AccountModel account = new AccountModel();
        DisplayProductModel display = new DisplayProductModel();
        CommonFunctions functions = new CommonFunctions();

        public DisplayProductModel Favorite(HttpContext httpContext, GrilledContext context)
        {
            account = context.Account.Where(a => a.Username == httpContext.Request.Cookies["Login"]).Include(a => a.Favorites).Include(a => a.Products).ThenInclude(b => b.Images).FirstOrDefault();

            display.Products = new List<ProductModel>();

            if (account == null)
                return display;

            if (account.Favorites == null)
                return display;

            foreach (FavoriteModel favorite in account.Favorites)
            {
                ProductModel product = context.Product.Where(p => p.Id == favorite.ProductId).Include(a => a.Images).FirstOrDefault();
                functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
            }
            return display;
        }

        public void Save(HttpContext httpContext, ProductModel product, GrilledContext context)
        {
            account = context.Account.Where(a => a.Username == httpContext.Request.Cookies["Login"]).Include(a => a.Favorites).Include(a => a.Products).ThenInclude(b => b.Images).FirstOrDefault();

            if (account.Favorites.FirstOrDefault(a => a.ProductId == product.Id) != null)
                context.Favorite.Remove(context.Favorite.Where(f => f.ProductId == product.Id).FirstOrDefault());

            else
            {
                FavoriteModel favorite = new FavoriteModel() { ProductId = product.Id };
                favorite.AddFavToAccount(account);
            }
            context.SaveChanges();
        }
        public DisplayProductModel Items(HttpContext httpContext, GrilledContext context)
        {
            account = context.Account.Include(a => a.Products).ThenInclude(b => b.Images).FirstOrDefault(a => a.Username == httpContext.Request.Cookies["Login"]);

            display.Products = new List<ProductModel>();

            if (account == null)
                return display;

            foreach (ProductModel product in account.Products)
            {
                functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
            }
            return display;
        }
        public bool Login(LoginModel login, HttpContext httpContext, GrilledContext context)
        {
            account = context.Account.FirstOrDefault(a => a.Username == login.Username);
            if (account != null && login.Password == account.Password)
            {
                httpContext.Response.Cookies.Append("Login", account.Username);
                return true;
            }
            return false;
        }
        public bool Register(RegistrationModel registration, GrilledContext context)
        {
            account = context.Account.FirstOrDefault(a => a.Username == registration.Username);
            if (account == null)
            {
                context.Account.Add(new AccountModel()
                {
                    Username = registration.Username,
                    Password = registration.Password,
                    Email = registration.Email
                });
                context.SaveChanges();

                return true; 
            }
            else
            {
                return false;  
            }
        }
    }
}
