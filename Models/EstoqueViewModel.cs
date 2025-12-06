using System.Collections.Generic;

namespace PapelArt.Models
{
    public class EstoqueViewModel
    {
        // filtros
        public string FiltroCodigo { get; set; }
        public string FiltroNome { get; set; }

        // resultados
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
