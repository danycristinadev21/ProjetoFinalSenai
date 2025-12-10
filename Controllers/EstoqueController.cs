using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PapelArt.Data;
using PapelArt.Models;

namespace PapelArt.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly PapelArtContext _context;

        public EstoqueController(PapelArtContext context)
        {
            _context = context;
        }

        // ✔ Tela 1 — Formulário de consulta
        public IActionResult Consulta()
        {
            return View();
        }

        // ✔ Tela 2 — Resultados filtrados
        public async Task<IActionResult> Resultados(string codigo, string nome, bool showAll = false)
        {
            var vm = new EstoqueViewModel
            {
                FiltroCodigo = codigo,
                FiltroNome = nome
            };

            IQueryable<Produto> query = _context.Produtos.AsNoTracking();

            if (!showAll)
            {
                if (!string.IsNullOrWhiteSpace(codigo))
                {
                    if (int.TryParse(codigo, out var id))
                        query = query.Where(p => p.Id == id);
                    else
                        query = query.Where(p =>
                            EF.Functions.Like(p.Id.ToString(), $"%{codigo}%") ||
                            EF.Functions.Like(p.Nome, $"%{codigo}%")
                        );
                }

                if (!string.IsNullOrWhiteSpace(nome))
                {
                    query = query.Where(p =>
                        EF.Functions.Like(p.Nome, $"%{nome}%")
                    );
                }
            }

            vm.Produtos = await query
                .OrderBy(p => p.Nome)
                .ToListAsync();

            return View(vm);
        }
    }
}

