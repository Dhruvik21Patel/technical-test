using ProductManagement.API.Helpers;
using ProductManagement.BusinessAccess.IService;
using ProductManagement.Common.Exceptions;
using ProductManagement.Common.Utils;
using ProductManagement.Entities.DTOModels.Request;
using ProductManagement.Entites.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IProductService _productService;

        public CategoryController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            ResponseHelper.SuccessResponse(await _productService.GetAllCategoriesAsync(), "Success");

    }
}