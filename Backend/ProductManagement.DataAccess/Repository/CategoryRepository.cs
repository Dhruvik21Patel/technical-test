using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.DataAccess.IRepository;
using ProductManagement.Entities.DataContext;
using ProductManagement.Entities.DataModels;

namespace ProductManagement.DataAccess.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context)
            : base(context)
        {
        }
    }

}