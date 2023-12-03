using TiendaWebAPI.Interfaces;

namespace TiendaWebAPI.Classes;
public record LoginResponse(string Email, string Token) : ILoginResponse;