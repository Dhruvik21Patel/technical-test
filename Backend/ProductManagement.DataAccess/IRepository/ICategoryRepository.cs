using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Entities.DataModels;

namespace ProductManagement.DataAccess.IRepository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
    }
}