namespace ProductManagement.BusinessAccess.Services
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using ProductManagement.BusinessAccess.IService;
    using ProductManagement.Common.Constants;
    using ProductManagement.Common.Exceptions;

    public class CurrentUserService : ICurrentUserService
    {
        #region Property
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion Property

        #region Methods
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion Method

        public int GetUserId()
        {
            Claim? userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new CustomException(401, ErrorMessages.UN_AUTHORIZATION);

            return int.Parse(userIdClaim.Value);
        }
    }
}