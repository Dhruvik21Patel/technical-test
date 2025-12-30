using ProductManagement.Entities.DataModels;

namespace ProductManagement.Entities.Seed
{
    public class DefaultProductCategory
    {
        public static List<ProductCategory> ProductCategoryList()
        {
            return new List<ProductCategory>
            {
                new ProductCategory
                {
                    ProductId = 1,
                    CategoryId = 1
                },
                new ProductCategory
                {
                    ProductId = 2,
                    CategoryId = 2
                },
                new ProductCategory
                {
                    ProductId = 3,
                    CategoryId = 3
                }
            };
        }
    }
}