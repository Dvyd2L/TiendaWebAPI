using Microsoft.AspNetCore.Mvc.Filters;

namespace TiendaWebAPI.Filters;

public class FiltroExcepcion : ExceptionFilterAttribute
{
    #region PROPs
    private readonly IWebHostEnvironment _env;
    #endregion

    #region CONSTRUCTORs
    public FiltroExcepcion(IWebHostEnvironment env) => _env = env;
    #endregion

    // Cuando haya errores inesperados en un controller vendrá siempre al método OnException
    // para dar respuesta al error. Nuestro trabajo pasa por integrar estoy en el proyecto,
    // en el Program, línea de AddControllers incluir la configuración de este filtro de excepción
    // ExceptionContext encapsula toda la información del error
    // En el constructor debemos inyectar otras dependencias que debemos usar, en este caso IWebHostEnvironment
    // porque vamos a registrar el error en 
    #region METHODs
    public override void OnException(ExceptionContext context)
    {
        string path = $@"{_env.ContentRootPath}\wwwroot\log-errores.txt";

        string IP = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        string metodo = context.HttpContext.Request.Method;
        string ruta = context.HttpContext.Request.Path.ToString();
        string error = context.Exception.Message;

        using (StreamWriter writer = new(path, append: true))
        {
            //writer.WriteLine(context.Exception.Source);
            //writer.WriteLine(context.Exception.Message);
            writer.WriteLine($@"{IP} - {metodo} - {ruta} - {error} - {DateTime.Now}");
        }

        base.OnException(context);
    }
    #endregion
}
