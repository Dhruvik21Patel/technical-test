namespace ProductManagement.BusinessAccess.Services
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using ProductManagement.BusinessAccess.IService;
    using ProductManagement.BusinessLayer.Services;
    using ProductManagement.Common.Constants;
    using ProductManagement.Common.Exceptions;
    using ProductManagement.DataAccess.IRepository;
    using ProductManagement.Entites.Request;
    using ProductManagement.Entities.DataModels;
    using ProductManagement.Entities.DTOModels.Request;
    using ProductManagement.Entities.DTOModels.Response;
    using ProductManagement.Entities.Response;
    public class ProductService : BaseService<Product>, IProductService
    {
        #region Property

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        #endregion Property

        #region Constructor

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork.ProductRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        #endregion Constructor

        #region Methods
        public async Task<PageResponse<ProductDTO>> GetAllProductAsync(PageRequest request)
        {
            PageResponse<Product>? result = await _unitOfWork.ProductRepository.GetAllAsyncPagination(
                pageIndex: request.PageNumber,
                pageSize: request.PageSize,

                filter: p =>
                    !p.IsDeleted &&
                    (string.IsNullOrEmpty(request.SearchKey)
                        || p.Name.Contains(request.SearchKey)
                        || p.Description.Contains(request.SearchKey)),

                orderBy: q =>
                    request.SortOrder == "desc"
                        ? q.OrderByDescending(x => x.CreatedAt)
                        : q.OrderBy(x => x.CreatedAt),

                include: query => query
                    .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
            );

            return new PageResponse<ProductDTO>(
                _mapper.Map<IEnumerable<ProductDTO>>(result.Result),
                result.PageNumber,
                result.PageSize,
                result.TotalPage,
                result.TotalRecords
            );
        }

        public async Task<ProductDTO?> GetByIdAsync(int id) =>
            _mapper.Map<ProductDTO>(await GetProductById(id));

        public async Task<ProductDTO> CreateAsync(ProductCreateUpdateRequestDTO dto)
        {
            await ValidateProductNameAsync(dto.Name);
            Product? product = _mapper.Map<Product>(dto);

            await ValidateAndMapCategoriesAsync(product, dto.CategoryIds);

            product.CreatedBy = _currentUserService.GetUserId();
            product.CreatedAt = DateTime.UtcNow;


            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProductDTO>(product);
        }


        public async Task<ProductDTO> UpdateAsync(int id, ProductCreateUpdateRequestDTO dto)
        {
            await ValidateProductNameAsync(dto.Name, id);

            Product? product = await GetProductById(id);
            await ValidateAndMapCategoriesAsync(product, dto.CategoryIds);

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;

            product.UpdatedBy = _currentUserService.GetUserId();
            product.UpdatedAt = DateTime.UtcNow;


            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> DeleteAsync(int id)
        {
            Product? product = await GetProductById(id);

            product.IsDeleted = true;
            product.DeletedBy = _currentUserService.GetUserId();
            product.DeletedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            IEnumerable<Category> categories = await _unitOfWork.CategoryRepository
                .GetAllAsync(x => !x.IsDeleted);

            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        #endregion Methods

        #region Private Methods

        private async Task<Product> GetProductById(int id)
        {
            return await _unitOfWork.ProductRepository
                .GetFirstOrDefaultAsyncWithInclude(
                    filter: p => p.Id == id && !p.IsDeleted,
                    include: query => query
                        .Include(p => p.ProductCategories)
                        .ThenInclude(pc => pc.Category)
                ) ?? throw new CustomException(404, "Product not found.");
        }

        private async Task ValidateProductNameAsync(string name, int? productId = null)
        {
            bool exists = await _unitOfWork.ProductRepository.AnyAsync(p =>
                !p.IsDeleted &&
                p.Name.ToLower() == name.Trim().ToLower() &&
                (!productId.HasValue || p.Id != productId.Value)
            );

            if (exists)
            {
                throw new CustomException(400, "Product with the same name already exists.");
            }
        }


        private async Task ValidateAndMapCategoriesAsync(
    Product product,
    List<int> categoryIds)
        {
            if (categoryIds == null || !categoryIds.Any())
                throw new CustomException(400, "At least one category is required.");

            List<int>? distinctIds = categoryIds.Distinct().ToList();

            IEnumerable<Category>? categories = await _unitOfWork.CategoryRepository
                .GetAllAsync(c => distinctIds.Contains(c.Id));

            List<int>? validIds = categories.Select(c => c.Id).ToList();

            List<int>? invalidIds = distinctIds.Except(validIds).ToList();

            if (categoryIds.Count != categoryIds.Distinct().Count())
            {
                throw new CustomException(400, "Duplicate category IDs are not allowed.");
            }

            if (invalidIds.Any())
            {
                throw new CustomException(
                    400,
                    $"Invalid category id(s): {string.Join(", ", invalidIds)}"
                );
            }

            product.ProductCategories = validIds
                .Select(id => new ProductCategory
                {
                    CategoryId = id
                })
                .ToList();
        }


        #endregion Private Methods
    }
}