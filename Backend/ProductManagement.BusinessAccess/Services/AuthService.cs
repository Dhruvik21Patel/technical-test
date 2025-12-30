namespace ProductManagement.BusinessAccess.Services
{
    using System.Security.Cryptography;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using ProductManagement.BusinessAccess.IService;
    using ProductManagement.BusinessAccess.Utilities;
    using ProductManagement.BusinessLayer.Services;
    using ProductManagement.Common.Constants;
    using ProductManagement.Common.Exceptions;
    using ProductManagement.Common.Utils;
    using ProductManagement.DataAccess.IRepository;
    using ProductManagement.Entities.DataModels;
    using ProductManagement.Entities.DTOModels;
    using ProductManagement.Entities.DTOModels.Request;
    using ProductManagement.Entities.Request;

    public class AuthService : BaseService<User>, IAuthService
    {
        #region Property

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSetting _jwtSettings;
        private readonly JwtTokenHelper _jwtHelper;

        #endregion Property

        #region Methods
        public AuthService(IUnitOfWork unitOfWork, JwtSetting jwtSettings, IMapper mapper, JwtTokenHelper jwtTokenHelper) : base(unitOfWork.AuthRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
            _jwtHelper = jwtTokenHelper;
        }
        #endregion Methods
        public async Task<TokenDTO> Login(AccountLoginDTO accountLoginDto)
        {
            User? user = await GetUserByEmail(accountLoginDto.Email);
            bool isValidPassword = accountLoginDto.Password != user.Password;
            if (isValidPassword)
                throw new CustomException(400, ErrorMessages.PASSWORD_INCORRECT);

            return GenerateTokenForUser(user, accountLoginDto);
        }

        private async Task<User?> GetUserByEmail(string email)
        {
            return await _unitOfWork.AuthRepository.GetFirstOrDefaultAsyncWithInclude(user => user.Email == email,
        include: query => query) ?? throw new CustomException(404, ErrorMessages.USER_NOT_FOUND);
        }

        private TokenDTO GenerateTokenForUser(User user, AccountLoginDTO accountLoginDto)
        {
            LoginDTO loginDTO = _mapper.Map<LoginDTO>(user);
            TokenDTO token = _jwtHelper.GenerateToken(_jwtSettings, loginDTO);
            return new TokenDTO
            {
                UserName = $"{user.FirstName} {user.LastName}",
                Token = token.Token,
                UserId = user.Id,
            };
        }
    }
}