
using Microsoft.EntityFrameworkCore;
using MVCWebApp.Models.Entities;

namespace MVCWebApp.DataBase

{   //classe para herdar os pacotes instalados : DbContext
    public class DbContextH: DbContext
    {
        //contrutor CTO usado dar opções de contexto do DB
        public DbContextH(DbContextOptions<DbContextH> options): base(options)
        {
            
        }
        //coleção de BB de tipo especifico
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Chama o método base

            modelBuilder.Entity<Fornecedor>()
                .Property(f => f.Segmento)
                .HasConversion<string>();
        }

    }
}
