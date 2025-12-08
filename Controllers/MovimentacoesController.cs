using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PapelArt.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PapelArt.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly PapelArtContext _context;

        public MovimentacoesController(PapelArtContext context)
        {
            _context = context;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var movimentacoes = _context.Movimentacoes
                .Include(m => m.Produto)
                .OrderByDescending(m => m.DataMovimentacao);

            return View(await movimentacoes.ToListAsync());
        }

        // GET: Movimentacoes/Create
        public IActionResult Create()
        {
            ViewBag.Produtos = new SelectList(
                _context.Produtos.ToList(),
                "Id",
                "Nome"
            );

            ViewBag.ListaProdutos = _context.Produtos
                .Select(p => new { p.Id, p.Preco })
                .ToList();

            return View();
        }

        // POST: Movimentacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movimentacao movimentacao)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Produtos = new SelectList(
                    _context.Produtos.ToList(),
                    "Id",
                    "Nome",
                    movimentacao.ProdutoId
                );

                ViewBag.ListaProdutos = _context.Produtos
                    .Select(p => new { p.Id, p.Preco })
                    .ToList();

                return View(movimentacao);
            }

            // Busca produto
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == movimentacao.ProdutoId);

            if (produto == null)
            {
                ModelState.AddModelError("", "Produto não encontrado.");

                ViewBag.Produtos = new SelectList(
                    _context.Produtos.ToList(),
                    "Id",
                    "Nome",
                    movimentacao.ProdutoId
                );

                ViewBag.ListaProdutos = _context.Produtos
                    .Select(p => new { p.Id, p.Preco })
                    .ToList();

                return View(movimentacao);
            }

            // ✅ AGORA FUNCIONA: VALORES
            movimentacao.ValorUnitario = produto.Preco;
            movimentacao.ValorTotal = movimentacao.Quantidade * produto.Preco;

            // Lógica de estoque
            if (movimentacao.Tipo == TipoMovimentacao.Entrada)
            {
                produto.Quantidade += movimentacao.Quantidade;
            }
            else if (movimentacao.Tipo == TipoMovimentacao.Saida)
            {
                if (produto.Quantidade < movimentacao.Quantidade)
                {
                    ModelState.AddModelError("", $"Estoque insuficiente. Disponível: {produto.Quantidade}");

                    ViewBag.Produtos = new SelectList(
                        _context.Produtos.ToList(),
                        "Id",
                        "Nome",
                        movimentacao.ProdutoId
                    );

                    ViewBag.ListaProdutos = _context.Produtos
                        .Select(p => new { p.Id, p.Preco })
                        .ToList();

                    return View(movimentacao);
                }

                produto.Quantidade -= movimentacao.Quantidade;
            }

            movimentacao.DataMovimentacao = DateTime.Now;

            _context.Movimentacoes.Add(movimentacao);
            _context.Produtos.Update(produto);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Usado para JS (se quiser usar futuramente)
        public IActionResult GetValorProduto(int id)
        {
            var produto = _context.Produtos.Find(id);

            return Json(new
            {
                preco = produto?.Preco ?? 0
            });
        }
    }
}






