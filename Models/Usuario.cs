using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(255)]
        [Column("senha")]
        public string Senha { get; set; } = string.Empty;

        [Required]
        [Column("nivel_acesso")]
        public string NivelAcesso { get; set; } = "usuario";
    }
}

