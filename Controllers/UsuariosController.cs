﻿using Microsoft.AspNetCore.Mvc;
using TiendaWebAPI.DTOs.Usuarios;
using TiendaWebAPI.Models;
using TiendaWebAPI.Services;

namespace TiendaWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController(
    MiTiendaContext context,
    HashService hashService,
    TokenService tokenService
    ) : ControllerBase
{
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] DTOUsuario input)
    {
        // Buscamos en BD para comprovar que no exista ningun usuario ya registrado con el email introducido
        Usuario? existeUsuario = await context.Usuarios.FindAsync(input.Email);

        if (existeUsuario is not null)
        {
            return BadRequest($"El email {input.Email} ya está registrado");
        }

        // Encriptamos la contraseña y recuperamos su hash y su salt
        Interfaces.IHashResult hashResult = hashService.GetHash(input.Password);

        // creamos el usuario que vamos a almacenar en BD 
        Usuario nuevoUsuario = new()
        {
            Email = input.Email,
            Password = hashResult.Hash,
            Salt = hashResult.Salt,
        };

        // añadimos el nuevo usuario a BD y guardamos los cambios
        _ = await context.Usuarios.AddAsync(nuevoUsuario);
        _ = await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] DTOUsuario input)
    {
        // Buscamos en BD para comprobar que exista un usuario registrado con el email introducido
        Usuario? usuarioBD = await context.Usuarios.FindAsync(input.Email);

        if (usuarioBD is null)
        {
            return NotFound($"El email {input.Email} no está registrado");
        }

        // Hasheamos el pasword proporcionado por el usuario con el salt guardado en BD
        Interfaces.IHashResult resultadoHash = hashService.GetHash(input.Password, usuarioBD.Salt);

        // Comporbamos que el resultado del hash coincida con el que ya teniamos almacenado en BD
        if (resultadoHash.Hash != usuarioBD.Password)
        {
            return Unauthorized("Credenciales incorrectas");
        }

        Interfaces.ILoginResponse loginResponse = tokenService.GenerarToken(input.Email);

        return Ok(loginResponse);
    }
}
