using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PapelArt.Data;
using PapelArt.Models;
using System.Threading.Tasks;

namespace PapelArt.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly PapelArtContext _context;

        // Construtor — injeta o contexto do banco
        public CategoriasController(PapelArtContext context)
        {
            _context = context;
        }

        // GET: Categorias (lista todas as categorias)
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.categorias.ToListAsync();
            return View(categorias);
        }

        // GET: Categorias/Create (abre o formulário)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create (salva no banco)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(categorias categoria)
{
    Console.WriteLine("[DEBUG] Valor recebido: " + categoria.nome);

    if (ModelState.IsValid)
    {
        _context.categorias.Add(categoria);
        _context.SaveChanges();
        Console.WriteLine("[DEBUG] Categoria salva com sucesso!");
        return RedirectToAction(nameof(Index));
    }

    Console.WriteLine("[DEBUG] ModelState inválido!");
    return View(categoria);
}
    }
}
