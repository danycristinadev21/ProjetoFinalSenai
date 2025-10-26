using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        public int Quantidade { get; set; }
        public int EstoqueMinimo { get; set; }
        public int EstoqueMaximo { get; set; }

        // Chave estrangeira
        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        // Relacionamento: um produto pode ter várias movimentações
        public ICollection<Movimentacao>? Movimentacoes { get; set; }
    }
}
