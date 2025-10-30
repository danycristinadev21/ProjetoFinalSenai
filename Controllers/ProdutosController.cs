using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PapelArt.Models;

namespace PapelArt.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly PapelArtContext _context;

        // Construtor: injeta o contexto do banco de dados
        public ProdutosController(PapelArtContext context)
        {
            _context = context;
        }

        // ===============================================================
        // GET: /Produtos
        // Lista todos os produtos com suas respectivas categorias
        // ===============================================================
        public async Task<IActionResult> Index()
        {
            var produtos = await _context.produtos
                .Include(p => p.categoria) // inclui os dados da categoria relacionada
                .ToListAsync();

            return View(produtos);
        }

        // ===============================================================
        // GET: /Produtos/Create
        // Exibe o formulário de cadastro de um novo produto
        // ===============================================================
        public IActionResult Create()
        {
            // Carrega as categorias para o dropdown no formulário
            ViewBag.categorias = _context.categorias.ToList();
            return View();
        }

        // ===============================================================
        // POST: /Produtos/Create
        // Salva o novo produto no banco
        // ===============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(produtos produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.categorias = _context.categorias.ToList();
            return View(produto);
        }

        // ===============================================================
        // GET: /Produtos/Edit/{id}
        // Exibe o formulário de edição de um produto existente
        // ===============================================================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var produto = await _context.produtos.FindAsync(id);
            if (produto == null) return NotFound();

            ViewBag.categorias = _context.categorias.ToList();
            return View(produto);
        }

        // ===============================================================
        // POST: /Produtos/Edit/{id}
        // Atualiza as informações do produto no banco
        // ===============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, produtos produto)
        {
            if (id != produto.id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.produtos.Any(p => p.id == produto.id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.categorias = _context.categorias.ToList();
            return View(produto);
        }

        // ===============================================================
        // GET: /Produtos/Delete/{id}
        // Exibe a tela de confirmação para excluir um produto
        // ===============================================================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var produto = await _context.produtos
                .Include(p => p.categoria)
                .FirstOrDefaultAsync(p => p.id == id);

            if (produto == null) return NotFound();

            return View(produto);
        }

        // ===============================================================
        // POST: /Produtos/Delete/{id}
        // Remove o produto definitivamente do banco
        // ===============================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.produtos.FindAsync(id);
            if (produto != null)
            {
                _context.produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ===============================================================
        // GET: /Produtos/Details/{id}
        // Exibe detalhes completos de um produto específico
        // ===============================================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var produto = await _context.produtos
                .Include(p => p.categoria)
                .FirstOrDefaultAsync(p => p.id == id);

            if (produto == null) return NotFound();

            return View(produto);
        }
    }
}
