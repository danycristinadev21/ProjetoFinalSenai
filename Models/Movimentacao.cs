using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    [Table("movimentacoes")] // mantém o nome real da tabela no banco
    public class Movimentacao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_produto")]
        public int ProdutoId { get; set; }

        public Produto? Produto { get; set; }

        [Required]
        [Column("tipo")]
        public string Tipo { get; set; } = "entrada"; // 'entrada' ou 'saida'

        [Required]
        [Column("quantidade")]
        public int Quantidade { get; set; }

        [Column("data_movimentacao")]
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;

        [Column("observacao")]
        public string? Observacao { get; set; }

        [Column("id_usuario")]
        public int? UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
