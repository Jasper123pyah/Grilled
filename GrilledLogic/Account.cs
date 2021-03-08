using System;
using System.Collections.Generic;
using System.Linq;
using GrilledCommon.Models;
using GrilledData;
using GrilledData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace GrilledLogic
{
    public class Account
    {
        DisplayProductModel display = new DisplayProductModel()
        {
            Products = new List<ProductModel>()
        };
        CommonFunctions functions = new CommonFunctions();
        AccountData accountData = new AccountData();

        public DisplayProductModel Favorite(HttpContext httpContext, GrilledContext context)
        {
            foreach (ProductModel product in accountData.GetAllFavorites(httpContext.Request.Cookies["Login"], context))
            {      
                functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
            }
            return display;
        }

        public void Save(HttpContext httpContext, ProductModel product, GrilledContext context)
        {
            accountData.AddFavorite(httpContext.Request.Cookies["Login"], product, context);
        }

        public DisplayProductModel Items(HttpContext httpContext, GrilledContext context)
        {
            foreach (ProductModel product in accountData.GetItems(httpContext.Request.Cookies["Login"], context))
            {
                functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
            }
            return display;
        }

        public bool Login(LoginModel login, HttpContext httpContext, GrilledContext context)
        {
            if (accountData.LoginCheck(login, context) != null)
            {
                httpContext.Response.Cookies.Append("Login", accountData.LoginCheck(login, context));
                httpContext.Response.Cookies.Append("Name", functions.GetAccountName(httpContext.Request.Cookies["Login"], context));
                return true;
            }
            return false;
        }

        public bool Register(RegistrationModel registration, GrilledContext context)
        {
            if (accountData.AddAccount(registration, context))
                return true;
            return false;
        }
    }
}
