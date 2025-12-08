using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelArt.Models
{
    public enum TipoMovimentacao
    {
        Entrada = 1,
        Saida = 2
    }

    [Table("movimentacoes")]
public class Movimentacao
{
    [Key]
    public int Id { get; set; }

    [Column("id_produto")]
    [Required]
    public int ProdutoId { get; set; }

    [ForeignKey("ProdutoId")]
    public Produto? Produto { get; set; }

    [Required]
    public TipoMovimentacao Tipo { get; set; }

    [Required]
    public int Quantidade { get; set; }

    [Column("data_movimentacao")]
    public DateTime DataMovimentacao { get; set; } = DateTime.Now;

    public string? Observacao { get; set; }

    [Column("valor_unitario")]
    public decimal ValorUnitario { get; set; }

    [Column("valor_total")]
    public decimal ValorTotal { get; set; }

    [Column("id_usuario")]
    public int? UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }
}

}


