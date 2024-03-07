using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using projeto_final.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace projeto_final.Services
{
    public class TokenGenerator
    {
        public string Generate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor{
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SECRETSECRET")),
                    SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddDays(1),
                Subject = AddClaims(user)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity AddClaims(User user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("UserId", user.UserId.ToString()));
            return claims;
        }
    }
}