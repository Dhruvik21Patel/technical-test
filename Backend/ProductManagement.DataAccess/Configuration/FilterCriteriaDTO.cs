using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProductManagement.Entites.Request;

namespace ProductManagement.DataAccess.Configuration
{
    public class FilterCriteriaDTO<T, TDTO>
    {
        public PageRequest Request { get; set; } = null!;
        public Expression<Func<T, bool>>? Filter { get; set; } = null;
        public Expression<Func<T, object>>? OrderBy { get; set; } = null;
        public Expression<Func<T, object>>? OrderByDescending { get; set; } = null;
        public List<Expression<Func<T, object>>>? IncludeExpressions { get; set; } = null;
        public string[]? ThenIncludeExpressions { get; set; } = null;
        public Expression<Func<T, bool>>? StatusFilter { get; set; } = null;
        public Expression<Func<T, TDTO>>? Select { get; set; } = null;
    }

}