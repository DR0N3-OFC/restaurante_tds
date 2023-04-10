using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Aula03.Models;

namespace Aula03.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EventsModel>? Events { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<BooksModel>? Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite("DataSource=tds.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Garcom>().ToTable("Garcom").HasKey(o => o.GarcomID);
            modelBuilder.Entity<Mesa>().ToTable("Mesa").HasKey(o => o.MesaID);
            modelBuilder.Entity<Produto>().ToTable("Produto").HasKey(o => o.ProdutoID);
            modelBuilder.Entity<Atendimento>().ToTable("Atendimento").HasKey(o => o.AtendimentoID);



            modelBuilder.Entity<Atendimento>()
                .HasMany(o => o.Produtos)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "Atendimentos",
                    e => e.HasOne<Produto>().WithMany().HasForeignKey("ProdutoID"),
                    j => j.HasOne<Atendimento>().WithMany().HasForeignKey("AtendimentoID"),
                    eJ =>
                    {
                        eJ.HasKey("AtendimentoID", "ProdutoID");
                        eJ.ToTable("Atendimentos");
                    });



            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Produto>();

        }
    }
}
