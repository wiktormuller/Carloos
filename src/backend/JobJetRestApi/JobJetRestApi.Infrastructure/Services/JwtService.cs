using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JobJetRestApi.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;
    private readonly RefreshTokenOptions _refreshTokenOptions;
    
    public JwtService(IOptions<JwtOptions> jwtOptions, IOptions<RefreshTokenOptions> refreshTokenOptions)
    {
        _jwtOptions = jwtOptions.Value;
        _refreshTokenOptions = refreshTokenOptions.Value;
    }

    public string GenerateAccessJwt(User user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
        claims.AddRange(roleClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddHours(_jwtOptions.ExpirationInHours);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Issuer,
            claims,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshJwt(User user, string ipAddress)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenOptions.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddHours(_refreshTokenOptions.ExpirationInHours);
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            _refreshTokenOptions.Issuer,
            _refreshTokenOptions.Issuer,
            claims,
            expires: expires,
            signingCredentials: credentials);

        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        return new RefreshToken(
            tokenAsString,
            user.Email,
            ipAddress,
            expires
            );
    }

    public ClaimsPrincipal GetClaimsPrincipalFromRefreshToken(string refreshToken)
    {
        var resultOfValidationRefreshToken = ValidateRefreshToken(refreshToken);

        if (! resultOfValidationRefreshToken.IsValid)
        {
            throw PassedRefreshTokenIsInvalidException.Default();
        }
        
        JwtSecurityToken jwtSecurityToken = resultOfValidationRefreshToken.ValidatedSecurityToken as JwtSecurityToken;
        if (jwtSecurityToken == null)
        {
            throw PassedRefreshTokenIsInvalidException.Default();
        }
        
        return resultOfValidationRefreshToken.ClaimsPrincipal;
    }
    
    private (bool IsValid, ClaimsPrincipal ClaimsPrincipal, SecurityToken ValidatedSecurityToken) ValidateRefreshToken(string refreshToken)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenOptions.Secret));
        
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = key,
            ValidIssuer = _refreshTokenOptions.Issuer,
            ValidAudience = _refreshTokenOptions.Issuer,
            ClockSkew = TimeSpan.Zero
        };

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

        try
        {
            var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
            return (true, claimsPrincipal, validatedToken);
        }
        catch (Exception)
        {
            return (false, default, default);
        }
    }
}