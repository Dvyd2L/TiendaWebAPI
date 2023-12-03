namespace TiendaWebAPI.DTOs.Dispositivos;

public class DTODispositivo
{
    public required string Nombre { get; set; }

    public decimal Precio { get; set; }

    public bool Descatalogado { get; set; }

    public int MarcaId { get; set; }
}
