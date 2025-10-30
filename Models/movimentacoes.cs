using System;
using System.ComponentModel.DataAnnotations;

namespace PapelArt.Models
{
    public class movimentacoes
    {
        public int id { get; set; }

        [Required]
        public int id_produto { get; set; }
        public produtos? produto { get; set; }

        [Required]
        public string tipo { get; set; } = "entrada"; // 'entrada' ou 'saida'

        [Required]
        public int quantidade { get; set; }

        public DateTime data_movimentacao { get; set; } = DateTime.Now;

        public string? observacao { get; set; }

        public int? id_usuario { get; set; }
        public usuarios? usuario { get; set; }
    }
}
