using TiendaWebAPI.DTOs.Dispositivos;

namespace TiendaWebAPI.DTOs.Marcas;

public class DTOMarcaCustom
{
    public int IdMarca { get; set; }
    public required string NombreMarca { get; set; }
    public required string NombreCategoria { get; set; }
    public decimal PromedioPrecio { get; set; }
    public int CuentaDispositivos { get; set; }
    public required List<DTODispositivoCustom> ListaDispositivos { get; set; }
}
