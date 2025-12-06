using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PapelArt.Data;       
using PapelArt.Models;     // Produto e EstoqueViewModel

namespace PapelArt.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstoqueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Estoque
        // parâmetros via query string: codigo, nome, showAll
        public async Task<IActionResult> Index(string codigo, string nome, bool showAll = false)
        {
            var vm = new EstoqueViewModel
            {
                FiltroCodigo = codigo,
                FiltroNome = nome
            };

            IQueryable<Produto> query = _context.Produtos.AsNoTracking();

            if (!showAll)
            {
                // se vier código (provavelmente numérico), filtra por Id ou por campo código.
                if (!string.IsNullOrWhiteSpace(codigo))
                {
                    // tenta parse para int; caso o código seja textual, usa Contains
                    if (int.TryParse(codigo, out var id))
                    {
                        query = query.Where(p => p.Id == id);
                    }
                    else
                    {
                        query = query.Where(p => EF.Functions.Like(p.Id.ToString(), $"%{codigo}%") || EF.Functions.Like(p.Nome, $"%{codigo}%"));
                    }
                }

                if (!string.IsNullOrWhiteSpace(nome))
                {
                    query = query.Where(p => EF.Functions.Like(p.Nome, $"%{nome}%"));
                }
            }
            // caso showAll == true, mantém query sem filtros e traz tudo

            vm.Produtos = await query
                .OrderBy(p => p.Nome)
                .ToListAsync();

            return View(vm);
        }
    }
}
