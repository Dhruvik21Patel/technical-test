using ProductManagement.DataAccess.IRepository;

namespace ProductManagement.DataAccess.IRepository
{
    public interface IUnitOfWork
    {
        IBaseRepository<T> GetRepository<T>() where T : class;

        int Save();

        Task<int> SaveAsync();

        IAuthRepository AuthRepository { get; }

        IProductRepository ProductRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        IOrderRepository OrderRepository { get; }
    }
}
