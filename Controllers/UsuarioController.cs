using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoupiAPI.Data;
using PoupiAPI.Model;

namespace PoupiAPI.Controllers;

public class UsuarioController : Controller
{
    private readonly PoupiAppDbContext _context;

    public UsuarioController(PoupiAppDbContext context)
    {
        _context = context;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> Cadastro(CadastroUsuarioDTO cadastro)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Email == cadastro.Email))
        {
            return BadRequest("Email já cadastrado.");
        }

        var usuario = new Usuario
        {
            Nome =  cadastro.Nome,
            Email = cadastro.Email,
            Senha = cadastro.Senha,
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return Ok("Usuário cadastrado com sucesso!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioDTO login)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == login.Email && u.Senha == login.Senha);

        if (usuario == null)
        {
            return Unauthorized("Email ou senha inválidos.");
        }

        return Ok("Login realizado!");
    }
}
