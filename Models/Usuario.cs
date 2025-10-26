using System.ComponentModel.DataAnnotations;

namespace PapelArt.Models
{
     // Representa o usuário que faz login no sistema
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        // Pode ser 'admin' ou 'usuario'
        [Required]
        public string NivelAcesso { get; set; } = "usuario";
    }
}