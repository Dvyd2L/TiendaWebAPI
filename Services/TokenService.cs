using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TiendaWebAPI.Classes;
using TiendaWebAPI.Interfaces;

namespace TiendaWebAPI.Services;

public class TokenService
{
    #region PROPs
    private readonly IConfiguration _configuration;
    #endregion

    #region CONSTRUCTORs
    public TokenService(IConfiguration configuration) => _configuration = configuration;
    #endregion

    #region METHODs
    public ILoginResponse GenerarToken(params string[] credenciales)
    //public ILoginResponse GenerarToken(string email)
    {
        string email = credenciales[0];

        // Los claims construyen la información que va en el payload del token
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, "Paco"), // ⚠️ puede que JL nos haga modificar esto
            new Claim(ClaimTypes.Country, "Jaen"), // ⚠️ puede que JL nos haga modificar esto
            new Claim(ClaimTypes.Gender, "MachoIberico"), // ⚠️ puede que JL nos haga modificar esto
            new Claim(ClaimTypes.Role, "Activo"), // ⚠️ puede que JL nos haga modificar esto
            new Claim("lo que yo quiera", "cualquier otro valor") // ⚠️ puede que JL nos haga modificar esto
        ];

        // Necesitamos la clave de generación de tokens
        string clave = _configuration["ClaveJWT"] ?? "";

        // Fabricamos el token
        SymmetricSecurityKey claveKey = new(Encoding.UTF8.GetBytes(clave));
        SigningCredentials signinCredentials = new(claveKey, SecurityAlgorithms.HmacSha256);

        // Le damos características
        JwtSecurityToken securityToken = new(
            claims: claims,
            expires: DateTime.Now.AddDays(30), // ⚠️ tiempo de expiracion, puede que JL nos haga modificar esto
            signingCredentials: signinCredentials
        );

        // Lo pasamos a string para devolverlo
        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new LoginResponse(email, tokenString);
    }
    #endregion
}

public class TokenCopilotService(string secretKey, string issuer, string audience)
{
    public string GenerateToken(string email)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        byte[] key = Encoding.ASCII.GetBytes(secretKey);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}