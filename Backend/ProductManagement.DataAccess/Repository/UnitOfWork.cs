using ProductManagement.DataAccess.HelperService;
using ProductManagement.DataAccess.IRepository;
using ProductManagement.Entities.DataContext;

namespace ProductManagement.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserResolverService _userResolverService;
        private readonly AppDbContext _dbContext;
        private IAuthRepository _authRepository;
        private IProductRepository _productRepository;

        private ICategoryRepository _categoryRepository;

        private IOrderRepository _orderRepository;
        public UnitOfWork(AppDbContext dbContext, UserResolverService userResolverService)
        {
            _dbContext = dbContext;
            _userResolverService = userResolverService;
        }
        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            return new BaseRepository<T>(_dbContext);

        }
        public int Save() => _dbContext.SaveChanges();
        public Task<int> SaveAsync() => _dbContext.SaveChangesAsync();

        public IAuthRepository AuthRepository
        {
            get
            {
                return _authRepository ??= new AuthRepository(_dbContext);
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository ??= new ProductRepository(_dbContext);
            }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(_dbContext);
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                return _orderRepository ??= new OrderRepository(_dbContext);
            }
        }
    }
}
