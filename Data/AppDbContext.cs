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
            modelBuilder.Entity<EventsModel>().ToTable("Events").HasKey(o => o.EventID);
            modelBuilder.Entity<User>().ToTable("Users").HasKey(o => o.ID);
            modelBuilder.Entity<BooksModel>().ToTable("Books").HasKey(o => o.BookID);


            modelBuilder.Entity<User>()
                .HasMany(o => o.Books)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserBooks",
                    e => e.HasOne<BooksModel>().WithMany().HasForeignKey("BookID"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("ID"),
                    eJ =>
                    {
                        eJ.HasKey("ID", "BookID");
                        eJ.ToTable("UserBooks");
                    });


            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Produto>();

        }
    }
}
