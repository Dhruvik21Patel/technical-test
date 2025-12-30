using ProductManagement.Entities.DataModels;

namespace ProductManagement.BusinessAccess.IService
{

    public interface IOrderService
    {
        Task<List<Order>> GetLatestOrdersPerCustomerAsync();
    }
}