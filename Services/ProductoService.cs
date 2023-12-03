using TiendaWebAPI.Interfaces;
using TiendaWebAPI.Models;

namespace TiendaWebAPI.Services;

public class ProductoService(MiTiendaContext DBcontext)
    : IDescatalogarProducto
{
    public async Task<Dispositivo?> DescatalogarProducto(int deviceId)
    {
        Dispositivo? deviceDB = await DBcontext.Dispositivos.FindAsync(deviceId);

        if (deviceDB is null)
        {
            return null;
        }

        deviceDB.Descatalogado = true; // false no JL

        _ = DBcontext.Dispositivos.Update(deviceDB);
        _ = await DBcontext.SaveChangesAsync();

        return deviceDB;
    }
}
