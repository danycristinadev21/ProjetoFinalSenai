using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PapelArt.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace PapelArt.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly PapelArtContext _context;

        public UsuariosController(PapelArtContext context)
        {
            _context = context;
        }

        // ========== LOGIN ==========

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                TempData["Error"] = "Preencha todos os campos.";
                return View();
            }

            // Criptografar a senha antes de comparar
            string senhaCriptografada = GerarHash(senha);

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(u => u.email == email && u.senha == senhaCriptografada);

            if (usuario != null)
            {
                TempData["Success"] = $"Bem-vindo(a), {usuario.nome}!";
                return RedirectToAction("Menu", "Home");
            }

            TempData["Error"] = "E-mail ou senha incorretos.";
            return View();
        }

        // ========== CADASTRO ==========

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(usuarios usuario)
        {
            Console.WriteLine($"[DEBUG] Tentando cadastrar usuário: {usuario.email}");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Por favor, preencha todos os campos corretamente.";
                return View("Cadastro", usuario);
            }

            bool existeEmail = await _context.usuarios.AnyAsync(u => u.email == usuario.email);
            if (existeEmail)
            {
                TempData["Error"] = "Este e-mail já está cadastrado.";
                return View("Cadastro", usuario);
            }

            // Criptografar senha antes de salvar
            usuario.senha = GerarHash(usuario.senha);
            usuario.nivel_acesso = "usuario";

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Usuário cadastrado com sucesso!";
            return RedirectToAction("Login");
        }

        // ========== MÉTODO AUXILIAR ==========
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
    }
}


