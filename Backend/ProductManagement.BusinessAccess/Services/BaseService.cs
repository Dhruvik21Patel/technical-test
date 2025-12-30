namespace ProductManagement.BusinessLayer.Services
{
    using ProductManagement.BusinessLayer.IService;
    using ProductManagement.DataAccess.IRepository;
    using System.Linq.Expressions;

    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task AddAsync(T entity) => await _repository.AddAsync(entity);

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter) => await _repository.GetFirstOrDefaultAsync(filter);

        public virtual async Task UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

        public virtual async Task AddRangeAsync(IEnumerable<T> models) => await _repository.AddRangeAsync(models);

        public void UpdateRange(IEnumerable<T> models)
            => _repository.UpdateRange(models);

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter) => await _repository.AnyAsync(filter);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select) => await _repository.GetAllAsync(filter, select);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter) => await _repository.GetAllAsync(filter);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes) => await _repository.GetAllAsync(filter, includes);
    }
}
