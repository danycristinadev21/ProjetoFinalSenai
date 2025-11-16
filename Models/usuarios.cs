using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    [Table("usuarios")]
    public class usuarios
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(100)]
        public string email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(255)]
        public string senha { get; set; } = string.Empty;

        [Required]
        [Column("nivel_acesso")]
        public string nivel_acesso { get; set; } = "usuario";
    }
}
