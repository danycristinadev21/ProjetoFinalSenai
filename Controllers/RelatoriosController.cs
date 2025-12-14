using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using PapelArt.Models;
using PapelArt.Data;
using Microsoft.AspNetCore.Authorization;

namespace PapelArt.Controllers
{
    [Authorize]
    public class RelatoriosController : Controller
    {
        private readonly PapelArtContext _context;

        public RelatoriosController(PapelArtContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Periodos = new SelectList(new[]
            {
                "", "Hoje", "Ontem", "Esta Semana", "Este Mês", "Últimos 30 dias", "Todos"
            }, null, "Selecione...");

            ViewBag.Produtos = new SelectList(
                _context.Produtos.Select(p => new { p.Id, p.Nome }).ToList(),
                "Id",
                "Nome",
                ""
            );

            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Gerar(string periodo, int? produtoId)
        {
            IQueryable<Movimentacao> query = _context.Movimentacoes
                .Include(m => m.Produto).ThenInclude(p => p.Categoria)
                .Include(m => m.Usuario)
                .OrderByDescending(m => m.DataMovimentacao);

            if (!string.IsNullOrEmpty(periodo) && periodo != "Todos")
            {
                var dataFiltro = CalcularPeriodo(periodo);
                query = query.Where(m => m.DataMovimentacao >= dataFiltro);
            }

            if (produtoId.HasValue && produtoId.Value > 0)
                query = query.Where(m => m.ProdutoId == produtoId.Value);

            var movimentacoes = await query.Take(200).ToListAsync();
            return View("Create", movimentacoes);
        }

        private DateTime CalcularPeriodo(string periodo)
        {
            return periodo switch
            {
                "Hoje" => DateTime.Today,
                "Ontem" => DateTime.Today.AddDays(-1),
                "Esta Semana" => DateTime.Today.AddDays(-7),
                "Este Mês" => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
                "Últimos 30 dias" => DateTime.Today.AddDays(-30),
                _ => DateTime.MinValue
            };
        }

        public IActionResult EstoqueAtual()
        {
            var estoque = _context.Produtos
                .Include(p => p.Categoria)
                .OrderBy(p => p.Categoria.Nome)
                .ThenBy(p => p.Nome)
                .ToList();
            return View(estoque);
        }
    }
}


