namespace ProductManagement.Entities.DTOModels.Request
{
    using System.ComponentModel.DataAnnotations;
    using ProductManagement.Common.Constants;
    public class AccountLoginDTO
    {
        [Required(ErrorMessage = ErrorMessages.EMPTY_EMAIL_ERROR_MESSAGE)]
        [EmailAddress(ErrorMessage = ErrorMessages.EMAIL_ERROR_MESSAGE)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = ErrorMessages.EMPTY_PASSWORD_ERROR_MESSAGE)]
        [RegularExpression(ValidationPatterns.PASSWORD_REGEX, ErrorMessage = ErrorMessages.PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; } = null!;
    }
}