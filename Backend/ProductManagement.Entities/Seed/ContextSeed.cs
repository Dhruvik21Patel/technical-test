using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities.DataModels;

namespace ProductManagement.Entities.Seed
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            CreateDefaultRecords(modelBuilder);
        }
        private static void CreateDefaultRecords(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(DefaultCategory.CategoryList());
            modelBuilder.Entity<Product>().HasData(DefaultProduct.ProductList());
            modelBuilder.Entity<ProductCategory>().HasData(DefaultProductCategory.ProductCategoryList());
            modelBuilder.Entity<User>().HasData(DefaultUser.IdentityList());
        }
    }
}