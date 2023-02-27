using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.RepositoriesInterfaces;
using OnlineShop.WebApi.Configurations;

namespace OnlineShop.WebApi.TokenHelpers;

public class JwtTokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;

    public JwtTokenService(JwtConfig jwtConfig)
    {   // value cannot be null
        // в противном случае может возникнуть проблема при запуске приложения
        // при отсутствии пользовательских секретов с JWT Token
        _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));
    }

    public string GenerateToken(Account account)
    {
        IClock clock = new Clock();
        var now = clock.GetCurrentTime();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            }),
            Expires = now.Add(_jwtConfig.LifeTime),
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public interface IClock
{
    DateTime GetCurrentTime();
}

public class Clock : IClock
{
    public DateTime GetCurrentTime() => DateTime.Now;
}