using Microsoft.EntityFrameworkCore;

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

        
    }
}

