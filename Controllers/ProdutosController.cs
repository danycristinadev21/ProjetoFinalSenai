using PapelArt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PapelArt.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly PapelArtContext _context;

        public ProdutosController(PapelArtContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public IActionResult Index()
        {
            var produtos = _context.Produtos
                .Include(p => p.Categoria)
                .ToList();

            return View(produtos);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            var categorias = _context.Categorias
                .Include(c => c.SubCategorias)
                .Where(c => c.CategoriaPaiId == null)
                .OrderBy(c => c.nome)
                .ToList();

            ViewBag.Categorias = categorias;

            return View();
        }


        // POST: Produtos/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Produto produto)
{
    if (ModelState.IsValid)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    ViewBag.Categorias = _context.Categorias
        .Include(c => c.SubCategorias)
        .Where(c => c.CategoriaPaiId == null)
        .ToList();

    return View(produto);
}


        // GET: Produtos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var produto = _context.Produtos.Find(id);
            if (produto == null) return NotFound();

            ViewBag.Categorias = _context.Categorias.ToList();
            return View(produto);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Produto produto)
        {
            if (id != produto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(produto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = _context.Categorias.ToList();
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var produto = _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);

            if (produto == null) return NotFound();

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Produtos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var produto = _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);

            if (produto == null) return NotFound();

            return View(produto);
        }
    }
}


