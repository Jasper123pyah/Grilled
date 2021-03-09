using GrilledCommon.Models;
using GrilledLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GrilledLogic
{
    public class JWTLogic
    { 
        private static JWTContainerModel GetJWTContainerModel(string name, string id)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.NameIdentifier, id)
                }
            };
        }
        public string GetId(string token)
        {
            IAuthService authservice = new JWTService(Environment.GetEnvironmentVariable("GRILLED_SECRET"));
            string name = authservice.GetTokenClaims(token).ToList().FirstOrDefault(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            return name;

        }
        public string GetToken(string name, string accountId)
        {
            IAuthContainerModel model = GetJWTContainerModel(name, accountId);
            IAuthService authService = new JWTService(model.SecretKey);

            string token = authService.GenerateToken(model);

            if (!authService.IsTokenValid(token))
                throw new UnauthorizedAccessException();

            return token;
        }
    }
}
