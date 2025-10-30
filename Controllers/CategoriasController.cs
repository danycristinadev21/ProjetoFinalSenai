using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PapelArt.Data;
using PapelArt.Models;
using System.Threading.Tasks;

namespace PapelArt.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Construtor: Injeção de Dependência para o Contexto do Banco de Dados
        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorias (Exibir a lista de todas as categorias)
        public async Task<IActionResult> Index()
        {
            // Busca todas as categorias no banco de forma assíncrona
            var categorias = await _context.categorias.ToListAsync();
            return View(categorias); // Passa a lista para a View
        }

        // GET: Categorias/Create (Exibir o formulário de criação)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create (Processar os dados do formulário)
        [HttpPost] // Indica que este método responde a submissões POST
        [ValidateAntiForgeryToken] // Proteção contra CSRF
        public async Task<IActionResult> Create([Bind("nome")] categorias categoria)
        {
            // Verifica se o modelo (Categoria) é válido conforme as anotações (ex: [Required])
            if (ModelState.IsValid)
            {
                // Adiciona o novo objeto ao contexto
                _context.Add(categoria);
                
                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();
                
                // Redireciona para a lista de categorias após a criação
                return RedirectToAction(nameof(Index));
            }

            // Se o modelo não for válido, retorna para a View com os dados preenchidos e erros
            return View(categoria);
        }
    }

    }