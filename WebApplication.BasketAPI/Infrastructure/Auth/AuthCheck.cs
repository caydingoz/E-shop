using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.BasketAPI.Infrastructure.Auth
{
    public static class AuthCheck
    {
        public static int GetCustomerId(string jwt)
        {
            string secureKey = "This is a very secure key";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false

            }, out SecurityToken validatedToken);
            var token = (JwtSecurityToken)validatedToken;
            string tokenPayloadJsonStr = token.Payload.SerializeToJson();
            JObject tokenPayloadJsonObj = JObject.Parse(tokenPayloadJsonStr);
            return tokenPayloadJsonObj["Id"].Value<Int32>();
        }
    }
}
