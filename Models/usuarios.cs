using System.ComponentModel.DataAnnotations;

namespace PapelArt.Models
{
     // Representa o usuário que faz login no sistema
    public class usuarios
    {
        public int id { get; set; }

        [Required, StringLength(100)]
        public string nome { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required]
        public string senha { get; set; } = string.Empty;

        // Pode ser 'admin' ou 'usuario'
        [Required]
        public string nivel_acesso { get; set; } = "usuario";
    }
}