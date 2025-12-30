namespace ProductManagement.DataAccess.Configuration
{
    using ProductManagement.Entites.Request;
    using System.Linq.Expressions;

    public class FilterCriteria<T>
    {
        public PageRequest Request { get; set; } = null!;
        public Expression<Func<T, bool>>? Filter { get; set; } = null;
        public Expression<Func<T, object>>? OrderBy { get; set; } = null;
        public Expression<Func<T, object>>? OrderByDescending { get; set; } = null;
        public List<Expression<Func<T, object>>>? IncludeExpressions { get; set; } = null;
        public string[]? ThenIncludeExpressions { get; set; } = null;
        public Expression<Func<T, bool>>? StatusFilter { get; set; } = null;
        public Expression<Func<T, T>>? Select { get; set; } = null;

    }
}
