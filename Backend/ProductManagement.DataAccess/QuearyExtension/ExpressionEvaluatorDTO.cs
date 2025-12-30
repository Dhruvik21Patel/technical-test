namespace ProductManagement.DataAccess.QuearyExtension
{
    using ProductManagement.DataAccess.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Dynamic.Core;
    public static class ExpressionEvaluatorDTO
    {
        public static async Task<(int count, IEnumerable<TDTO> data)> EvaluatePageQuery<T, TDTO>(this IQueryable<T> query, FilterCriteriaDTO<T, TDTO> criteria) where T : class where TDTO : class
        {
            query = IncludeExpressions(query, criteria);
            query = FilterQuery(query, criteria);
            query = ApplySort(query, criteria);

            int count = await query.CountAsync();

            if (criteria.Request is not null)
                query = query.ApplyQuery(criteria.Request);

            return (count, ApplySelect(query, criteria));
        }

        private static IEnumerable<TDTO> ApplySelect<T, TDTO>(IQueryable<T> query, FilterCriteriaDTO<T, TDTO> criteria) where T : class where TDTO : class
            => query.Select(criteria.Select).AsEnumerable();

        private static IQueryable<T> FilterQuery<T, TDTO>(IQueryable<T> query, FilterCriteriaDTO<T, TDTO> criteria) where T : class where TDTO : class
        {
            if (criteria.StatusFilter is not null) query = query.Where(criteria.StatusFilter);
            if (criteria.Filter is not null) query = query.Where(criteria.Filter);
            return query;
        }

        private static IQueryable<T> ApplySort<T, TDTO>(IQueryable<T> query, FilterCriteriaDTO<T, TDTO> criteria) where T : class where TDTO : class
        {
            if (string.IsNullOrEmpty(criteria.Request.SortBy)) return query;

            string sortExpression = criteria.Request.SortBy.Trim();
            string sortOrder = criteria.Request.SortOrder;
            string dynamicSortExpression = $"{sortExpression} {sortOrder}";

            query = query.OrderBy(dynamicSortExpression);

            return query;
        }

        private static IQueryable<T> IncludeExpressions<T, TDTO>(IQueryable<T> query, FilterCriteriaDTO<T, TDTO> criteria) where T : class where TDTO : class
        {
            if (criteria.IncludeExpressions is null) return query;

            query = criteria.IncludeExpressions.Aggregate(query, (current, include) =>
            {
                return current.Include(include);
            });

            if (criteria.ThenIncludeExpressions != null)
            {
                query = criteria.ThenIncludeExpressions.Aggregate(query, (current, thenInclude) =>
                {
                    return current.Include(thenInclude);
                });
            }

            return query;
        }
    }
}
