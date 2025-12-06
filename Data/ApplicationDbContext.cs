using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PapelArt.Models; // Garante acesso às suas classes de modelo

namespace PapelArt.Data
{
    // A classe do contexto deve herdar de DbContext
     public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("produtos");
            modelBuilder.Entity<Categoria>().ToTable("categorias");
            modelBuilder.Entity<Movimentacao>().ToTable("movimentacoes");
            modelBuilder.Entity<Usuario>().ToTable("usuarios");

            base.OnModelCreating(modelBuilder);
        }
    }
}