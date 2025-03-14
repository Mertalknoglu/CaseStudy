using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;

namespace Product.Infrastructure.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // **Decimal Alanları Tanımlama**
            modelBuilder.Entity<Products>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");

            // **String Alanlar için Maksimum Uzunluk Tanımlama**
            modelBuilder.Entity<Products>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Products>()
                .Property(p => p.Description)
                .HasMaxLength(500);

            // **Integer Alanlar**
            modelBuilder.Entity<Products>()
                .Property(p => p.Stock)
                .IsRequired();
        }
    }
}
