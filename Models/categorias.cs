using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    
    [Table("categorias")] // nome exato da tabela no banco
    public class categorias
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [Column("nome")]
        public string nome { get; set; } = string.Empty;

        // Relacionamento 1:N
        public ICollection<produtos>? produto { get; set; }
    }
}
