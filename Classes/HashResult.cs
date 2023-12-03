using TiendaWebAPI.Interfaces;

namespace TiendaWebAPI.Classes;

public record HashResult(string Hash, byte[] Salt) : IHashResult;