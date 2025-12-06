using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                // Guarda dados na sessão
                HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
                HttpContext.Session.SetString("UsuarioNivel", usuario.NivelAcesso);

                // Redireciona para a página inicial
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Erro = "E-mail ou senha inválidos.";
            return View();
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
