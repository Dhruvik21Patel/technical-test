namespace ProductManagement.API.Controllers
{
    using ProductManagement.API.Helpers;
    using ProductManagement.BusinessAccess.IService;
    using ProductManagement.Common.Exceptions;
    using ProductManagement.Common.Utils;
    using ProductManagement.Entities.DTOModels.Request;
    using ProductManagement.Entites.Request;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest request) =>
            ResponseHelper.SuccessResponse(await _productService.GetAllProductAsync(request), "Success");

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            ResponseHelper.SuccessResponse(await _productService.GetByIdAsync(id), "Success");

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateUpdateRequestDTO dto)
        {
            if (!ModelState.IsValid) throw new ModelStateException(ModelState);

            return ResponseHelper.SuccessResponse(await _productService.CreateAsync(dto), "Product added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateUpdateRequestDTO dto)
        {
            if (!ModelState.IsValid) throw new ModelStateException(ModelState);

            return ResponseHelper.SuccessResponse(await _productService.UpdateAsync(id, dto), "Product updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ResponseHelper.SuccessResponse(await _productService.DeleteAsync(id), "Product deleted successfully.");
        }
    }
}