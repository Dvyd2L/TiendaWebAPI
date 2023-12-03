using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaWebAPI.DTOs.Dispositivos;
using TiendaWebAPI.DTOs.Marcas;
using TiendaWebAPI.Models;
using TiendaWebAPI.Services;

namespace TiendaWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DispositivosController(
    MiTiendaContext context,
    ProductoService productoService
    ) : ControllerBase
{
    #region GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dispositivo>>> Get()
    {
        Dispositivo[]? result = await context.Dispositivos.ToArrayAsync();

        return result is null
            ? NotFound("No se encontro ningun dispositivo")
            : Ok(result);
    }

    [HttpGet("{pk}")]
    public async Task<ActionResult<Dispositivo>> GetByPK([FromRoute] int pk)
    {
        Dispositivo? result = await context.Dispositivos.FindAsync(pk);

        return result is null
            ? NotFound($"No se encontro ningun dispositivo con ID {pk}")
            : Ok(result);
    }

    [HttpGet("marca/{brand}")]
    public async Task<ActionResult<IEnumerable<Dispositivo>>> GetByBrand([FromRoute] string brand)
    {
        Dispositivo[]? result = await context.Dispositivos.Where((x) => x.Marca.Nombre == brand).ToArrayAsync();

        return result is null
            ? NotFound($"No se encontro ningun dispositivo de la marca {brand}")
            : Ok(result);
    }

    [HttpGet("custom")]
    public async Task<ActionResult<IEnumerable<Dispositivo>>> GetCustom()
    {
        DTOMarcaCustom[] result = await context.Dispositivos
            .Select((device) => new DTOMarcaCustom()
            {
                IdMarca = device.MarcaId,
                NombreMarca = device.Marca.Nombre,
                NombreCategoria = device.Marca.Categoria.Nombre,
                PromedioPrecio = device.Marca.Dispositivos.Average((d) => d.Precio),
                CuentaDispositivos = device.Marca.Dispositivos.Count,
                ListaDispositivos = device.Marca.Dispositivos
                    .Select((dispositivo) => new DTODispositivoCustom()
                    {
                        IdDispositivo = dispositivo.Id,
                        NombreDispositivo = dispositivo.Nombre,
                        Precio = dispositivo.Precio,
                    }).ToList()
            }).ToArrayAsync();

        return Ok(result);
    }
    #endregion END GET

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DTODispositivo input)
    {
        Marca? brandDB = await context.Marcas.FindAsync(input.MarcaId);

        if (brandDB is null)
        {
            return NotFound($"No se encontró ninguna marca con ID {input.MarcaId}");
        }

        Dispositivo newDevice = new()
        {
            Nombre = input.Nombre,
            Descatalogado = input.Descatalogado,
            Precio = input.Precio,
            MarcaId = input.MarcaId,
        };

        _ = await context.Dispositivos.AddAsync(newDevice);
        _ = await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{pk}")]
    public async Task<IActionResult> Put([FromRoute] int pk, [FromBody] DTODispositivo input)
    {
        Dispositivo? deviceDB = await context.Dispositivos.FindAsync(pk);

        if (deviceDB is null)
        {
            return NotFound($"No se encontró ningun dispositivo con ID {pk}");
        }

        Marca? brandDB = await context.Marcas.FindAsync(input.MarcaId);

        if (brandDB is null)
        {
            return NotFound($"No se encontró ninguna marca con ID {input.MarcaId}");
        }

        deviceDB.Nombre = input.Nombre;
        deviceDB.Descatalogado = input.Descatalogado;
        deviceDB.Precio = input.Precio;
        deviceDB.MarcaId = input.MarcaId;

        _ = context.Dispositivos.Update(deviceDB);
        _ = await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{pk}")]
    public async Task<IActionResult> Patch([FromRoute] int pk)
    {
        Dispositivo? updatedDevice = await productoService.DescatalogarProducto(pk);

        return updatedDevice is null
            ? NotFound($"No existe ningun dispositivo con ID {pk}")
            : Ok(updatedDevice);
    }

    [HttpDelete("{pk}")]
    public async Task<IActionResult> Delete([FromRoute] int pk)
    {
        Dispositivo? deviceDB = await context.Dispositivos.FindAsync(pk);

        if (deviceDB is null)
        {
            return NotFound($"No se encontró ningun dispositivo con ID {pk}");
        }

        if (!deviceDB.Descatalogado)
        {
            return BadRequest("No puedes eliminar dispositivos que no esten descatalogados");
        }

        _ = context.Dispositivos.Remove(deviceDB);
        _ = await context.SaveChangesAsync();

        return NoContent();
    }
}
