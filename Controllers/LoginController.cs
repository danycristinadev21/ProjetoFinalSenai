//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PapelArt.Models;
using Microsoft.EntityFrameworkCore;

namespace PapelArt.Controllers
{
    public class LoginController : Controller
    {
        private readonly PapelArtContext _context;

        public LoginController(PapelArtContext context)
        {
            _context = context;
        }

        // Exibe a tela de login
        public IActionResult Index()
        {
            return View();
        }

        // Faz a validação do login
        [HttpPost]
        public async Task<IActionResult> Index(string email, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

            if (usuario != null)
            {
                // ✅ NOVO: Cria CLAIMS com ID do usuário
                var claims = new List<Claim>
                {
                    new Claim("Id", usuario.Id.ToString()),           // ✅ ID para GetUsuarioLogadoId()
                    new Claim(ClaimTypes.Name, usuario.Nome),         // Nome
                    new Claim(ClaimTypes.Email, usuario.Email),       // Email
                    new Claim("NivelAcesso", usuario.NivelAcesso)     // Seu campo customizado
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Mantém login após fechar navegador
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                };

                // ✅ AUTENTICAÇÃO COM COOKIES (resolve o problema do ID=1)
                await HttpContext.SignInAsync(
    "CookieAuth",  // ← MESMO NOME do Program.cs!
    new ClaimsPrincipal(claimsIdentity),
    authProperties);

                // ✅ MANTER SESSÃO (compatibilidade com código existente)
                HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
                HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
                HttpContext.Session.SetString("UsuarioNivel", usuario.NivelAcesso);

HttpContext.Items["UsuarioLogadoId"] = usuario.Id;
                 
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Erro = "E-mail ou senha inválidos.";
            return View();
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            // ✅ LOGOUT COMPLETO (cookies + sessão)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            HttpContext.Items.Remove("UsuarioLogadoId");
            return RedirectToAction("Index", "Login");
        }

 
    
     }
}

