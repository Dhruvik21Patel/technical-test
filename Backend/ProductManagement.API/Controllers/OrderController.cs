using ProductManagement.API.Helpers;
using ProductManagement.BusinessAccess.IService;
using ProductManagement.Common.Exceptions;
using ProductManagement.Common.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestOrders()
        {
            var result = await _orderService.GetLatestOrdersPerCustomerAsync();
            var finalResult = new
            {
                count = result.Count,
                data = result
            };
            return ResponseHelper.SuccessResponse(finalResult, "Success");
        }
    }
}