using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    public class produtos
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string nome { get; set; }

        public string descricao { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal preco { get; set; }

        public int quantidade { get; set; }

        public int estoque_minimo { get; set; }

        public int estoque_maximo { get; set; }

        [Column("id_categoria")] // 👈 Aqui garantimos o nome da coluna do banco
        public int? id_categoria { get; set; }

        [ForeignKey("id_categoria")]
        public categorias categoria { get; set; }
    }
}
