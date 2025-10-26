using System;
using System.ComponentModel.DataAnnotations;

namespace PapelArt.Models
{
    public class Movimentacao
    {
        public int Id { get; set; }

        [Required]
        public int Id_Produto { get; set; }
        public Produto? Produto { get; set; }

        [Required]
        public string Tipo { get; set; } = "entrada"; // 'entrada' ou 'saida'

        [Required]
        public int Quantidade { get; set; }

        public DateTime Data_Movimentacao { get; set; } = DateTime.Now;

        public string? Observacao { get; set; }

        public int? Id_Usuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
