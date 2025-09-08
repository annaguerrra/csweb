using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MockTestCs.Services.JWT;

public class EFJWTService : IJWTService
{
    public string CreateToken(UserToLoginDto data)
    {
        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
        var keyBytes = Encoding.UTF8.GetBytes(jwtSecret);
        var key = new SymmetricSecurityKey(keyBytes);

        var jwt = new JwtSecurityToken(
            claims: [
                new Claim(ClaimTypes.NameIdentifier, data.ID.ToString()),
                new Claim(ClaimTypes.Name, data.Username)
            ],
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature
            )
        );
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(jwt);
    }
}