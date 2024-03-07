using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdaTech.AIntelligence.Service.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(string user, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("3h9RtE2F#pW!b5Z^Kx)vDc6S7GyP4NqX")),
                    SecurityAlgorithms.HmacSha256)
            };
            var chaveToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(chaveToken);
        }

    }
}
