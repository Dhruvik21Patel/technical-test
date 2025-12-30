namespace ProductManagement.DataAccess.HelperService
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using ProductManagement.Common.Constants;
    using ProductManagement.Common.Exceptions;

    public class UserResolverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserResolverService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserID => int.Parse(FindClaim(ClaimTypes.NameIdentifier));

        public string UserName => FindClaim(ClaimTypes.Name);

        private string? FindClaim(string claimName)
        {
            ClaimsIdentity? claimsIdentity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            bool isAuthenticated = _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

            if (isAuthenticated is not true) throw new CustomException(401, ErrorMessages.UN_AUTHORIZATION);

            Claim? claim = claimsIdentity?.FindFirst(claimName);
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }
    }
}