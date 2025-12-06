using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    [Table("categorias")]
    public class Categoria
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        // CATEGORIA PAI (se for subcategoria)
        [Column("categoria_pai_id")]
        public int? CategoriaPaiId { get; set; }

        [ForeignKey("CategoriaPaiId")]
        public Categoria? CategoriaPai { get; set; }

        // SUBCATEGORIAS (filhas)
        public ICollection<Categoria>? SubCategorias { get; set; }

        // Produtos ligados a essa categoria
        public ICollection<Produto>? Produtos { get; set; }
    }
}



