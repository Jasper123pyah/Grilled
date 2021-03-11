using GrilledCommon.Models;
using GrilledLogic.Managers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GrilledLogic
{
    public static class GetLoginName
    {
        public static string Name(HttpContext context)
        {
            string token = context.Request.Cookies["Grilled_Token_Login"];
            if (token != null)
            {
                IAuthService authservice = new JWTService(Environment.GetEnvironmentVariable("GRILLED_SECRET"));
                string name = authservice.GetTokenClaims(token).ToList().FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name)).Value;

                if (name == null)
                    return null;

                else
                    return name;
            }
            return null;
        }
    }
}
