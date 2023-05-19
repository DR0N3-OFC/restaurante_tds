using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Aula03.Models;

namespace Aula03.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Waiter>? Waiters { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ServiceProduct>? ServiceProducts { get; set; }
        public DbSet<Table>? Tables { get; set; }
        public DbSet<Service>? Services { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //        => options.UseSqlite("DataSource=tds.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Waiter>().ToTable("Waiter").HasKey(o => o.WaiterID);
            modelBuilder.Entity<Table>().ToTable("Table").HasKey(o => o.TableID);
            modelBuilder.Entity<Category>().ToTable("Category").HasKey(o => o.CategoryID);
            modelBuilder.Entity<Product>().ToTable("Product").HasKey(o => o.ProductID);
            modelBuilder.Entity<ServiceProduct>().ToTable("ServiceProduct").HasKey(o => o.ServiceProductID);
            modelBuilder.Entity<Service>().ToTable("Service").HasKey(o => o.ServiceID);



            modelBuilder.Entity<Service>()
                .HasMany(o => o.ServiceProducts)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceXServiceProducts",
                    e => e.HasOne<ServiceProduct>().WithMany().HasForeignKey("ServiceProductID"),
                    j => j.HasOne<Service>().WithMany().HasForeignKey("ServiceID"),
                    eJ =>
                    {
                        eJ.HasKey("ServiceID", "ServiceProductID");
                        eJ.ToTable("ServiceXServiceProducts");
                    });



            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Produto>();

        }
    }
}
