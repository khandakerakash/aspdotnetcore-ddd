using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL
{
    public class ApplicationDbContext : DbContext
    {
        protected ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrandProduct>().HasKey(bp => new { bp.BrandId, bp.ProductId });
            
            modelBuilder.Entity<BrandProduct>()
                .HasOne<Brand>(bp => bp.Brand)
                .WithMany(p => p.BrandProducts)
                .HasForeignKey(bp => bp.BrandId);

            modelBuilder.Entity<BrandProduct>()
                .HasOne<Product>(bp => bp.Product)
                .WithMany(p => p.BrandProducts)
                .HasForeignKey(bp => bp.ProductId);
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BrandProduct> BrandProducts { get; set; }
    }
}