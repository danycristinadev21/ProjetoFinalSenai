using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    [Table("produtos")] // nome da tabela no banco
    public class Produto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("descricao")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Column("preco")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Column("quantidade")]
        public int Quantidade { get; set; }

        [Column("estoque_minimo")]
        public int EstoqueMinimo { get; set; }

        [Column("estoque_maximo")]
        public int EstoqueMaximo { get; set; }

        // FK obrigatória para categoria
        [Required(ErrorMessage = "Selecione uma categoria.")]
        [Column("id_categoria")]
        [ForeignKey("categoria")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }
    }
}

