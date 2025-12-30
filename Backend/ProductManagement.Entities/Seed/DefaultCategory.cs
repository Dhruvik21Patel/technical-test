using ProductManagement.Entities.DataModels;

namespace ProductManagement.Entities.Seed
{
    public class DefaultCategory
    {
        public static List<Category> CategoryList()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Electronics"
                },
                new Category
                {
                    Id = 2,
                    Name = "Clothing"
                },
                new Category
                {
                    Id = 3,
                    Name = "Books"
                }
            };
        }
    }
}