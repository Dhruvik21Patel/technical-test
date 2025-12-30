namespace ProductManagement.DataAccess.IRepository
{
  using System.Linq.Expressions;
  using ProductManagement.Common.Constants;
  using ProductManagement.DataAccess.Configuration;
  using ProductManagement.Entites.Request;
  using ProductManagement.Entities.Response;

  public interface IBaseRepository<T> where T : class
  {
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

    Task<T?> GetFirstOrDefaultAsyncWithInclude(
      Expression<Func<T, bool>> filter,
      Func<IQueryable<T>, IQueryable<T>>? include = null,
      CancellationToken cancellationToken = default);
    IQueryable<T> GetAll();

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<T> models);

    void UpdateRange(IEnumerable<T> models);

    Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes);
    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select);

    void Delete(T entity);
   
    Task<PageResponse<T>> GetAllAsyncPagination(int pageIndex = 1, int pageSize = 10, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string sortDirection = SystemConstants.ASCENDING, string sortColumn = "Id", Func<IQueryable<T>, IQueryable<T>>? include = null);
  }
}
