using ProductManagement.Entites.Request;
using ProductManagement.Entities.DataModels;
using ProductManagement.Entities.DTOModels.Request;
using ProductManagement.Entities.DTOModels.Response;
using ProductManagement.Entities.Response;

namespace ProductManagement.BusinessAccess.IService
{

    public interface IProductService
    {
        Task<PageResponse<ProductDTO>> GetAllProductAsync(PageRequest request);
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(ProductCreateUpdateRequestDTO dto);
        Task<ProductDTO> UpdateAsync(int id, ProductCreateUpdateRequestDTO dto);
        Task<ProductDTO> DeleteAsync(int id);

        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    }
}