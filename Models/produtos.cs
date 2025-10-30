using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    [Table("produtos")] // nome da tabela no banco
    public class produtos
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [Column("nome")]
        public string nome { get; set; } = string.Empty;

        [Column("descricao")]
        public string? descricao { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Column("preco")]
        [DataType(DataType.Currency)]
        public decimal preco { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Column("quantidade")]
        public int quantidade { get; set; }

        [Column("estoque_minimo")]
        public int estoque_minimo { get; set; }

        [Column("estoque_maximo")]
        public int estoque_maximo { get; set; }

        // FK obrigatória para categoria
        [Required(ErrorMessage = "Selecione uma categoria.")]
        [Column("id_categoria")]
        [ForeignKey("categoria")]
        public int id_categoria { get; set; }

        public categorias? categoria { get; set; }
    }
}

