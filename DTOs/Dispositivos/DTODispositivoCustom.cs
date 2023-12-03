namespace TiendaWebAPI.DTOs.Dispositivos;

public class DTODispositivoCustom
{
    public int IdDispositivo { get; set; }
    public required string NombreDispositivo { get; set; }
    public decimal Precio { get; set; }
}
