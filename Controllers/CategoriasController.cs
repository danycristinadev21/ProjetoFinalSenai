using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;   // ✅ ESSENCIAL (SelectList)
using Microsoft.EntityFrameworkCore;
using PapelArt.Data;                        // ✅ Usando ApplicationDbContext
using PapelArt.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace PapelArt.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly PapelArtContext _context;

        // Construtor — injeta o contexto do banco
        public CategoriasController(PapelArtContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.Categorias
                .Include(c => c.CategoriaPai)
                .ToListAsync();

            return View(categorias);
        }

        // GET: Categorias/Create
public IActionResult Create()
{
    // força execução da query agora com ToList() para uso seguro no SelectList
    var categoriasPai = _context.Categorias
        .Where(c => c.CategoriaPaiId == null)
        .OrderBy(c => c.Nome)
        .ToList();

    ViewBag.CategoriasPai = new SelectList(categoriasPai, "Id", "Nome");
    return View();
}

// POST: Categorias/Create
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Nome,CategoriaPaiId")] Categoria categoria)
{
    if (ModelState.IsValid)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        // opcional: para depuração, você pode mostrar qual id pai foi salvo
        // TempData["Msg"] = $"Categoria salva. PaiId = {categoria.CategoriaPaiId}";

        return RedirectToAction(nameof(Index));
    }

    // Se houver erro, recarrega o select mantendo a seleção anterior
    var categoriasPai = _context.Categorias
        .Where(c => c.CategoriaPaiId == null)
        .OrderBy(c => c.Nome)
        .ToList();

    ViewBag.CategoriasPai = new SelectList(categoriasPai, "Id", "Nome", categoria.CategoriaPaiId);
    return View(categoria);
}

    }
}
