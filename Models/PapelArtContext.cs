using Microsoft.EntityFrameworkCore;

namespace PapelArt.Models
{
    // Classe que representa o banco de dados
    public class PapelArtContext : DbContext
    {
        public PapelArtContext(DbContextOptions<PapelArtContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
    }
}
