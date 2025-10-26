using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PapelArt.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        // Relacionamento: uma categoria pode ter vários produtos
        public ICollection<Produto>? Produtos { get; set; }
    }
}