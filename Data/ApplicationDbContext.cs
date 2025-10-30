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
        // Construtor: Essencial para a injeção de dependência na inicialização do app
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets: Representam as tabelas do seu banco de dados
        // Você deve ter um DbSet para cada tabela/modelo
        
        public DbSet<categorias> categorias { get; set; }
        public DbSet<produtos> produtos { get; set; }
        public DbSet<movimentacoes> movimentacoes { get; set; }
        public DbSet<usuarios> usuarios { get; set; }

        

    }
}