using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace GrilledCommon.Models
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public int ExpireMinutes { get; set; } = 10080; //7 days
        public string SecretKey { get; set; } = Environment.GetEnvironmentVariable("GRILLED_SECRET") ?? "Epicgamingmomentwhichisalsoextremelypoggers";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public Claim[] Claims { get; set; }
    }
}
