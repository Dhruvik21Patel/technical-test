namespace ProductManagement.BusinessAccess.IService
{
    using ProductManagement.Entities.DTOModels.Request;
    using ProductManagement.Entities.Request;

    public interface IAuthService
    {
        Task<TokenDTO> Login(AccountLoginDTO accountLoginDto);
    }
}