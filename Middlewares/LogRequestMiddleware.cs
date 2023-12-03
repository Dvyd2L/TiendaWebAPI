namespace TiendaWebAPI.Middlewares;

public class LogRequestMiddleware
{
    #region PROPs
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env; // no se utiliza, probablemente podamos eliminarlo en ete caso
    #endregion

    #region CONSTRUCTORs
    /// <summary>
    /// Equipamos al middleware con lo que va a necesitar. 
    /// Para que un middleware de paso al siguiente, debemos inyectar RequestDelegate y llamar
    /// a la propiedad que lo coge como _next (podría tener otro nombre)
    /// En este caso necesitamos IWebHostEnvironment para poder acceder a información del sistema de carpetas del servidor
    /// </summary>
    /// <param name="next">Para que un middleware de paso al siguiente</param>
    /// <param name="env">Para poder acceder a información del sistema de carpetas del servidor</param>
    public LogRequestMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }
    #endregion

    #region METHODs
    /// <summary>
    /// Invoke o InvokeAsync
    /// Este método se va a ejecutar automáticamente en cada petición porque en el program hemos registrado el middleware así:
    /// <![CDATA[app.UseMiddleware<IpBlockerMiddleware>();]]>
    /// </summary>
    /// <param name="httpContext">tiene información de la petición que viene</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        string IP = httpContext.Connection.RemoteIpAddress?.ToString() ?? "NotAvailable";
        string metodo = httpContext.Request.Method;
        string logFile = $@"{_env.ContentRootPath}\wwwroot\log-request.txt";

        using (StreamWriter writer = new(logFile))
        {
            writer.WriteLine($"{DateTime.Now} - {IP} - {metodo}");
        }

        await _next(httpContext);
    }
    #endregion
}
