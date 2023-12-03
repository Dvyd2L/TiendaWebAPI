using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TiendaWebAPI.Filters;
using TiendaWebAPI.Middlewares;
using TiendaWebAPI.Models;
using TiendaWebAPI.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// recuperamos la cadena de conexion (connectionStrings) que guardamos en appsettings.Development.json como "DefaultConnection"
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

#region SERVICEs
builder.Services
    .AddControllers(options =>
    {
        // Integramos el filtro de excepción para todos los controladores
        _ = options.Filters.Add<FiltroExcepcion>();
    })
    .AddJsonOptions(options =>
    {
        // Configuramos JSONSerializer para evitar referencias cíclicas
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// añadimos el nuestro DBcontext a nuestra aplicacion web
builder.Services
    .AddDbContext<MiTiendaContext>(options =>
    {
        // Configuramos la cadena de conexion a la BD
        _ = options.UseSqlServer(connectionString);

        // Deshabilita el tracking a nivel de proyecto (NoTracking).
        //Por defecto siempre hace el tracking. Con esta configuración, no.
        // En cada operación de modificación de datos en los controladores,
        // deberemos habilitar el tracking en cada operación
        //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
    });

builder.Services.AddTransient<ProductoService>();
builder.Services.AddTransient<HashService>();
builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ClaveJWT"] ?? ""))
       });

#region CORS Policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        _ = builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
#endregion 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});
#endregion

WebApplication app = builder.Build();

#region MIDDLEWAREs
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseMiddleware<LogRequestMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors();
#endregion

app.MapControllers();

app.Run();
