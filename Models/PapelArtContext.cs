using Microsoft.EntityFrameworkCore;
using PapelArt.Models;

namespace PapelArt.Models
{
    public class PapelArtContext : DbContext
    {
        public PapelArtContext(DbContextOptions<PapelArtContext> options)
            : base(options)
        {
        }

        // DbSets CORRETOS (singular / PascalCase)
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeia para os nomes reais das tabelas do banco (minúsculo / plural)
            modelBuilder.Entity<Produto>().ToTable("produtos");
            modelBuilder.Entity<Categoria>().ToTable("categorias");
            modelBuilder.Entity<Movimentacao>().ToTable("movimentacoes");
            modelBuilder.Entity<Usuario>().ToTable("usuarios");

            base.OnModelCreating(modelBuilder);
        }
    }
}


