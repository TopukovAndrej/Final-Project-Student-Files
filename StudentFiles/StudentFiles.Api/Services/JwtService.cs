namespace StudentFiles.Api.Services
{
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using StudentFiles.Api.Configuration;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateJwtToken(Guid userUid, string userName, string userRole)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var claims = new[]
            {
                new Claim(type: JwtRegisteredClaimNames.Sub, value: userUid.ToString()),
                new Claim(type: JwtRegisteredClaimNames.Email, value: userName),
                new Claim(type: ClaimTypes.Role, value: userRole),
                new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                new Claim(type: JwtRegisteredClaimNames.Iat,
                          value: DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                          valueType: ClaimValueTypes.Integer64)
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(key: key, algorithm: SecurityAlgorithms.HmacSha256)
            };

            var token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(token: token);
        }
    }
}
