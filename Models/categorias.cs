using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    
    public class categorias
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string nome { get; set; }

        // Relacionamento 1:N
        public ICollection<produtos> produto { get; set; }
    }
}
