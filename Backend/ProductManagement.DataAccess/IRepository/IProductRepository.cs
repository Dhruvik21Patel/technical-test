namespace ProductManagement.DataAccess.IRepository
{
    using ProductManagement.Entities.DataModels;
    using ProductManagement.Entites.Request;

    public interface IProductRepository : IBaseRepository<Product>
    {
    }
}