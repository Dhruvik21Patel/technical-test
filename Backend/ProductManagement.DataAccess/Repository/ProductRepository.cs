namespace ProductManagement.DataAccess.Repository
{
    using ProductManagement.DataAccess.IRepository;
    using ProductManagement.DataAccess.Repository;
    using ProductManagement.Entities.DataContext;
    using ProductManagement.Entities.DataModels;
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

    }
}