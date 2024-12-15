using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project12_JwtToken.JWT
{
    public class TokenGenerator
    {
        public string GenerateJwtToken(string username,string email,string name, string surname)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("20Derste20ProjeToken+-*/1234tokenJWT")); //token ın içine oluşturdugum imza
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); //security key ve token olustururken kullanacagım şifreleme algoritmasını tutar
            var claimsExample = new[]
            {//tokenımın temel parametrelerini tutar
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim(JwtRegisteredClaimNames.Email,email),
                new Claim(JwtRegisteredClaimNames.GivenName,name),
                new Claim(JwtRegisteredClaimNames.FamilyName,surname),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "localhost",//token ın yayıncısı,kim tarafından olusturuldu token
                audience: "localhost", //token ı kim dinliyor
                claims: claimsExample, //token ın parametreleri nerden geliyor
                expires: DateTime.Now.AddMinutes(5),//token ne zamana kadar geçerli, burada olusturuldutan 5 dk sonra kapanıyor
                signingCredentials: credentials); //tokenın şifreleme algoritması gibi parametreleri neler

            return new JwtSecurityTokenHandler().WriteToken(token); //token olusmus oldu
        }

        public string GenerateJwtToken2(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("20Derste20ProjeToken+-*/1234tokenJWT")); //token ın içine oluşturdugum imza
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); //security key ve token olustururken kullanacagım şifreleme algoritmasını tutar
            var claimsExample = new[]
            {//tokenımın temel parametrelerini tutar
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "localhost",//token ın yayıncısı,kim tarafından olusturuldu token
                audience: "localhost", //token ı kim dinliyor
                claims: claimsExample, //token ın parametreleri nerden geliyor
                expires: DateTime.Now.AddMinutes(5),//token ne zamana kadar geçerli, burada olusturuldutan 5 dk sonra kapanıyor
                signingCredentials: credentials); //tokenın şifreleme algoritması gibi parametreleri neler

            return new JwtSecurityTokenHandler().WriteToken(token); //token olusmus oldu
        }
    }
}
