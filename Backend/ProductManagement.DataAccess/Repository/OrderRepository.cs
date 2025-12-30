namespace ProductManagement.DataAccess.Repository
{
    using ProductManagement.DataAccess.IRepository;
    using ProductManagement.Entities.DataContext;
    using ProductManagement.Entities.DataModels;
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

    }
}