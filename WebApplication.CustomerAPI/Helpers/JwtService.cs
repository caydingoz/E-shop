using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication.CustomerAPI.Helpers
{
    public class JwtService
    {
        private string secureKey = "This is a very secure key";
        public string Generate(int Id)
        {
            var symetricSecureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey)); //Algoritma ile key uret
            var credentials = new SigningCredentials(symetricSecureKey, SecurityAlgorithms.HmacSha256Signature); //Kimlik olustur
            var header = new JwtHeader(credentials); //header icin hazirla
            var payload = new JwtPayload {
                { "Id", Id }, {"exp", ((DateTimeOffset)DateTime.Today.AddDays(1)).ToUnixTimeSeconds()}
            };
            var securityToken = new JwtSecurityToken(header, payload); //token objesi olustur

            return new JwtSecurityTokenHandler().WriteToken(securityToken); //token hazir
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateAudience = false, 
                ValidateIssuer = false

            }, out SecurityToken validatedToken) ;
            return (JwtSecurityToken)validatedToken;
        }
    }
}
