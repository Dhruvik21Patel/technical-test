namespace ProductManagement.BusinessAccess.Utilities
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using ProductManagement.Common.Utils;
    using ProductManagement.Entities.DTOModels;
    using ProductManagement.Entities.Request;
    using Microsoft.AspNetCore.Http;
    using Microsoft.IdentityModel.Tokens;

    public class JwtTokenHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtTokenHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public TokenDTO GenerateToken(JwtSetting jwtSetting, LoginDTO user)
        {
            if (jwtSetting == null)
                return null;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim("aud", jwtSetting.Audience)

        };
            var token = new JwtSecurityToken(

            jwtSetting.Issuer,

            jwtSetting.Audience,

            claims,

            expires: DateTime.UtcNow.AddMilliseconds(jwtSetting.ExpiryMilliseconds),

            signingCredentials: credentials);

            string tokengen = new JwtSecurityTokenHandler().WriteToken(token);
            SetJwt(tokengen);
            TokenDTO dto = new TokenDTO() { Token = tokengen };
            return dto;
        }

        public void SetJwt(string encryptedtoken)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("x-access-token", encryptedtoken,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                });
        }
    }
}