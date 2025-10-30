using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PapelArt.Models;

namespace PapelArt.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly PapelArtContext _context;

        public ProdutosController(PapelArtContext context)
        {
            _context = context;
        }

        // GET: Produtos (listar todos)
        public IActionResult Index()
        {
            var produtos = _context.produtos
                .Include(p => p.categoria)
                .ToList();
            return View(produtos);
        }

        // GET: Produtos/Create
public IActionResult Create()
{
    ViewBag.Categorias = _context.categorias.ToList();
    return View();
}

// POST: Produtos/Create
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(produtos produto)
{
    Console.WriteLine("[DEBUG] Valor recebido: " + produto.nome);

    if (!ModelState.IsValid)
    {
        Console.WriteLine("[DEBUG] ModelState inválido!");
        foreach (var erro in ModelState)
        {
            if (erro.Value.Errors.Count > 0)
                Console.WriteLine($"[ERRO] Campo: {erro.Key} -> {erro.Value.Errors[0].ErrorMessage}");
        }

        ViewBag.Categorias = _context.categorias.ToList();
        return View(produto);
    }

    try
    {
        _context.Add(produto);
        await _context.SaveChangesAsync();
        TempData["MensagemSucesso"] = "Produto cadastrado com sucesso!";
        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        Console.WriteLine("[ERRO] Falha ao salvar produto: " + ex.Message);
        ViewBag.Categorias = _context.categorias.ToList();
        return View(produto);
    }
}


        // GET: Produtos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var produto = _context.produtos.Find(id);
            if (produto == null) return NotFound();

            ViewBag.Categorias = _context.categorias.ToList();
            return View(produto);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, produtos produto)
        {
            if (id != produto.id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(produto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = _context.categorias.ToList();
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var produto = _context.produtos
                .Include(p => p.categoria)
                .FirstOrDefault(p => p.id == id);

            if (produto == null) return NotFound();

            return View(produto);
        }

        // POST: Produtos/Delete/5 (confirma exclusão)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var produto = _context.produtos.Find(id);
            if (produto != null)
            {
                _context.produtos.Remove(produto);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Produtos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var produto = _context.produtos
                .Include(p => p.categoria)
                .FirstOrDefault(p => p.id == id);

            if (produto == null) return NotFound();

            return View(produto);
        }
    }
}

