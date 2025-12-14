using PapelArt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PapelArt.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly PapelArtContext _context;

        public ProdutosController(PapelArtContext context)
        {
            _context = context;
        }

        // GET: Produtos
        // Ex.: Index action
        public IActionResult Index()
        {
            // Carrega produtos com categoria e a categoria pai (se existir)
            var produtos = _context.Produtos
                .Include(p => p.Categoria)
                    .ThenInclude(c => c.CategoriaPai)   // garante CategoriaPai carregada
                .ToList();

            return View(produtos);
        }


        // GET: Produtos/Create
        public IActionResult Create()
        {
            var categorias = _context.Categorias
                .Include(c => c.SubCategorias)
                .Where(c => c.CategoriaPaiId == null)
                .OrderBy(c => c.Nome)
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

                TempData["MensagemSucesso"] = "PRODUTO CADASTRADO COM SUCESSO!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nome");
            return View(produto);
        }



        // GET: Produtos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var produto = _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);

            if (produto == null)
                return NotFound();

            ViewBag.Categorias = _context.Categorias
                .OrderBy(c => c.Nome)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoriaPai != null
                           ? c.CategoriaPai.Nome + " → " + c.Nome
                           : c.Nome
                })
                .ToList();

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

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nome");
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                TempData["ProdutoExcluido"] = "PRODUTO EXCLUÍDO COM SUCESSO!";  // ✅ ADICIONE ESTA LINHA
            }

            return RedirectToAction("Index", "Produtos");
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


