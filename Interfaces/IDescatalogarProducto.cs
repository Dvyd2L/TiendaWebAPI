using TiendaWebAPI.Models;

namespace TiendaWebAPI.Interfaces;

public interface IDescatalogarProducto
{
    public Task<Dispositivo?> DescatalogarProducto(int deviceId);
}
