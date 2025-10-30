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

        
        public DbSet<produtos> produtos { get; set; }
        public DbSet<categorias> categorias { get; set; }
        public DbSet<movimentacoes> movimentacoes { get; set; }
        public DbSet<usuarios> usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define os nomes exatos das tabelas e colunas no banco
            modelBuilder.Entity<produtos>().ToTable("produtos");
            modelBuilder.Entity<categorias>().ToTable("categorias");
            modelBuilder.Entity<movimentacoes>().ToTable("movimentacoes");
            modelBuilder.Entity<usuarios>().ToTable("usuarios");
        }
    }
}

