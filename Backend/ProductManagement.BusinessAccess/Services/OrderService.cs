using ProductManagement.BusinessAccess.IService;
using ProductManagement.BusinessLayer.Services;
using ProductManagement.DataAccess.IRepository;
using ProductManagement.Entities.DataModels;

namespace ProductManagement.BusinessAccess.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        #region Property

        private readonly IUnitOfWork _unitOfWork;

        #endregion Property

        #region Constructor

        public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork.OrderRepository)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion Constructor
        public async Task<List<Order>> GetLatestOrdersPerCustomerAsync()
        {
            IEnumerable<Order>? orders = await _unitOfWork.OrderRepository
                .GetAllAsync(o => !o.IsDeleted);

            return orders
                .GroupBy(o => o.CustomerId)
                .Select(g => g
                    .OrderByDescending(x => x.OrderDate)
                    .First())
                .ToList();
        }
    }
}