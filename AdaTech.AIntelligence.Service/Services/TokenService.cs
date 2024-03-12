using AdaTech.AIntelligence.DateLibrary.Context;
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
        private readonly IConfiguration _configuration;
        private readonly ExpenseReportingDbContext _expenseReportingDbContext;
        public TokenService(IConfiguration configuration, ExpenseReportingDbContext expenseReportingDbContext)
        {
            _configuration = configuration;
            _expenseReportingDbContext = expenseReportingDbContext;

        }

        public async Task<string> GenerateToken(UserInfo user)
        {
            var userRoles = from ur in _expenseReportingDbContext.UserRoles
                            join r in _expenseReportingDbContext.Roles on ur.RoleId equals r.Id
                            where ur.UserId == user.Id
                            select r.Name;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("IsSuperUser", user.IsSuperUser.ToString()),
                new Claim("IsStaff", user.IsStaff.ToString()),
                new Claim("IsActive", user.IsActive.ToString()),
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (string.IsNullOrEmpty(encodedToken))
            {
                throw new InvalidOperationException("Não foi possível gerar o token de autenticação.");
            }

            return encodedToken;
        }
    }
}
