namespace ProductManagement.API.Controllers
{
    using ProductManagement.API.Helpers;
    using ProductManagement.BusinessAccess.IService;
    using ProductManagement.Common.Exceptions;
    using ProductManagement.Common.Utils;
    using ProductManagement.Entities.DTOModels.Request;
    using ProductManagement.Entities.Request;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginDTO accountLoginDto)
        {
            if (!ModelState.IsValid) throw new ModelStateException(ModelState);
            TokenDTO token = await _authService.Login(accountLoginDto);
            return ResponseHelper.SuccessResponse(token, MessageUtils.SUCCESS_LOGIN);
        }
    }
}