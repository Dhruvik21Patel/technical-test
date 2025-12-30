namespace ProductManagement.DataAccess.Repository
{
    using ProductManagement.DataAccess.Configuration;
    using ProductManagement.DataAccess.IRepository;
    using ProductManagement.DataAccess.QuearyExtension;
    using ProductManagement.Entities.DataContext;
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;
    using ProductManagement.Entities.Response;
    using ProductManagement.Entites.Request;
    using ProductManagement.Common.Constants;

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
            await _dbSet.AddAsync(entity, cancellationToken);

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default) => await _dbSet.FirstOrDefaultAsync(filter, cancellationToken);

        public virtual async Task<T?> GetFirstOrDefaultAsyncWithInclude(
    Expression<Func<T, bool>> filter,
    Func<IQueryable<T>, IQueryable<T>>? include = null,
    CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(filter, cancellationToken);
        }


        public virtual IQueryable<T> GetAll()
            => _dbSet.AsNoTracking().AsQueryable();


        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> models)
           => await _dbSet.AddRangeAsync(models);

        public virtual void UpdateRange(IEnumerable<T> models)
            => _dbSet.UpdateRange(models);

        public virtual void Delete(T entity) => _dbSet.Remove(entity);

        public async Task<(int count, IEnumerable<T> data)> GetPaginationWithExpressions(FilterCriteria<T> criteria)
        => await GetAll().AsNoTracking().EvaluatePageQuery(criteria);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default) => _dbSet.AnyAsync(filter, cancellationToken);
        public async Task<IEnumerable<T>> GetAllAsync()
           => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
            => await _dbSet
                .AsNoTracking()
                .Where(filter)
                .ToListAsync(cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes)
        {
            IQueryable<T> query = GetAll();

            query = query.Where(filter);

            if (includes is not null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select)
        {
            IQueryable<T> query = GetAll().Where(filter).Select(select);
            return await query.ToListAsync();
        }

        public async Task<PageResponse<T>> GetAllAsyncPagination(
                                                int pageIndex = 1,
                                                int pageSize = 10,
                                                Expression<Func<T, bool>>? filter = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                string sortDirection = SystemConstants.ASCENDING,
                                                string sortColumn = "Id",
                                                Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (include != null)
                query = include(query);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = sortDirection.Equals(SystemConstants.DESCENDING, StringComparison.CurrentCultureIgnoreCase)
                    ? query.OrderByDescending(e => EF.Property<object>(e, sortColumn))
                    : query.OrderBy(e => EF.Property<object>(e, sortColumn));
            }

            int totalCount = await query.CountAsync();
            List<T> items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResponse<T>(items, pageIndex, pageSize, totalPage: (int)Math.Ceiling((double)totalCount / pageSize), totalCount);
        }
    }
}
