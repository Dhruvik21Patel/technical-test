using AutoMapper;
using ProductManagement.DataAccess.IRepository;
using ProductManagement.Entities.DataModels;
using ProductManagement.Entities.DTOModels.Response;

namespace ProductManagement.BusinessAccess.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
    }
}