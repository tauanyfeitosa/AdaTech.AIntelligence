using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdaTech.AIntelligence.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfigurationSection _tokenSettings;
        public TokenService(IConfiguration configuration)
        {
            _tokenSettings = configuration.GetSection("TokenSettings");
        }

        public (string Token, DateTime Expiration) GenerateToken(UserInfo user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings["SecretKey"]!);
            var privateKey = new SymmetricSecurityKey(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(privateKey,
                    SecurityAlgorithms.HmacSha256)
            };
            var chaveToken = tokenHandler.CreateToken(tokenDescriptor);

            return (tokenHandler.WriteToken(chaveToken), chaveToken.ValidFrom);
        }
    }
}
