namespace Deskstones.LMS.Domain.Services
{
    using Deskstones.LMS.Domain.Interface;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    internal class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly string _jwtSecret = configuration["Jwt:Secret"] ?? throw new InvalidOperationException("Jwt:Secret not found in configuration");
        private readonly string _issuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer not found in configuration");
        private readonly string _audience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience not found in configuration");
        public string GenerateToken(string userId, string userEmail, string userRole, string RegisterationDate)
        {
            var claims = new[]
            {
            new Claim("user id", userId),
            new Claim("user role", userRole),
            new Claim("registeration date", RegisterationDate),
            new Claim(JwtRegisteredClaimNames.Email, userEmail),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
