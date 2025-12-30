namespace ProductManagement.Common.Constants
{
    public class ValidationPatterns
    {
        public const string PASSWORD_REGEX = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$";
    }
}