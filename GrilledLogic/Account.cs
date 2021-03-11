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
        JWTLogic jwtLogic = new JWTLogic();

        public DisplayProductModel Favorite(HttpContext httpContext, GrilledContext context)
        {
            foreach (ProductModel product in accountData.GetAllFavorites(jwtLogic.GetId(httpContext.Request.Cookies["Grilled_Token_Login"]), context))
            {      
                functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
            }
            return display;
        }

        public void Save(HttpContext httpContext, ProductModel product, GrilledContext context)
        {
            accountData.AddFavorite(jwtLogic.GetId(httpContext.Request.Cookies["Grilled_Token_Login"]), product, context);
        }

        public DisplayProductModel Items(HttpContext httpContext, GrilledContext context)
        {
            foreach (ProductModel product in accountData.GetItems(jwtLogic.GetId(httpContext.Request.Cookies["Grilled_Token_Login"]), context))
            {
                functions.AddToDisplay(product, context, functions.GetImagePath(httpContext), display.Products);
            }
            return display;
        }

        public bool Login(LoginModel login, HttpContext httpContext, GrilledContext context)
        {
            string accountId = accountData.LoginCheck(login, context);
            CookieOptions cookieOptions = new CookieOptions()
            { 
                Expires = new DateTimeOffset(DateTime.Now.AddDays(7))
            };

            if (accountId != null)
            {
                string tokenstring = jwtLogic.GetToken(login.Username, accountId);
                httpContext.Response.Cookies.Append("Grilled_Token_Login", tokenstring, cookieOptions);
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
