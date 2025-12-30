using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities.DataModels;
using ProductManagement.Entities.Seed;

namespace ProductManagement.Entities.DataContext
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Order>(entity =>
                {
                    entity.HasKey(o => o.OrderId);

                    entity.HasOne(o => o.Customer)
                        .WithMany(u => u.UserOrders)
                        .HasForeignKey(o => o.CustomerId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasIndex(o => o.CustomerId);
                });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();
        }
    }
}