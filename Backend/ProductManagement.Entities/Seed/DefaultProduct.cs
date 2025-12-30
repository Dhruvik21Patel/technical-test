using ProductManagement.Entities.DataModels;

namespace ProductManagement.Entities.Seed
{
    public class DefaultProduct
    {
        public static List<Product> ProductList()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "Gaming Laptop",
                    Price = 75000,
                    StockQuantity = 10
                },
                new Product
                {
                    Id = 2,
                    Name = "T-Shirt",
                    Description = "Cotton T-Shirt",
                    Price = 799,
                    StockQuantity = 50
                },
                new Product
                {
                    Id = 3,
                    Name = "C# Book",
                    Description = "ASP.NET Core Guide",
                    Price = 599,
                    StockQuantity = 30
                }
            };
        }
    
    }
}