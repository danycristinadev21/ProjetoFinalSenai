using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PapelArt.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


[AllowAnonymous] // ou só nas actions de login, veja abaixo
public class UsuariosController : Controller
{
    private readonly PapelArtContext _context;

    public UsuariosController(PapelArtContext context)
    {
        _context = context;
    }

    // GET: /Usuarios/Login
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View(); // Views/Usuarios/Login.cshtml
    }

    // POST: /Usuarios/Login
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string email, string senha)
    {
        // 1) Gera o hash da senha digitada
        var senhaHash = GerarHash(senha);

        // 2) Compara com o hash gravado no banco
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senhaHash);


        if (usuario == null)
        {
            ViewBag.Erro = "E-mail ou senha inválidos.";
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim("Id", usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim("NivelAcesso", usuario.NivelAcesso)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims,
            "CookieAuth" // mesmo nome do Program.cs
        );

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };

        await HttpContext.SignInAsync(
            "CookieAuth",
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        // se ainda usa sessão:
        HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
        HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
        HttpContext.Session.SetString("UsuarioNivel", usuario.NivelAcesso);

        // depois do login → vai para o Menu
        return RedirectToAction("Menu", "Home");
    }

    private string GerarHash(string input)
    {
        using (var sha = SHA256.Create())
        {
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("CookieAuth");
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}



