namespace ProductManagement.BusinessLayer.Profiles
{
    using AutoMapper;
    using ProductManagement.Entities.DTOModels.Response;
    using Entities.DataModels;
    using Entities.DTOModels;
    using ProductManagement.Entities.DTOModels.Request;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginDTO>()
            .ForMember(dest => dest.Email, source => source.MapFrom(src => src.Email))
            .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, source => source.MapFrom(src => $"{src.FirstName} {src.LastName}")).ReverseMap();

            CreateMap<ProductCreateUpdateRequestDTO, Product>()
    .ForMember(dest => dest.ProductCategories, opt => opt.Ignore());

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
                    src.ProductCategories == null
                        ? new List<CategoryDTO>()
                        : src.ProductCategories
                            .Where(pc => pc.Category != null)
                            .Select(pc => new CategoryDTO
                            {
                                Id = pc.Category.Id,
                                Name = pc.Category.Name
                            }).ToList()
                ));

            CreateMap<Category, CategoryDTO>();

        }
    }
}
