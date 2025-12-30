namespace ProductManagement.Common.Constants
{
    public class ErrorMessages
    {

        public static class ExceptionMessage
        {
            public const string INTERNAL_SERVER = "An error occurred while processing the request";

            public const string INVALID_MODELSTATE = "Invalid Entry";


        }

        public const string UN_AUTHORIZATION = "Un authorization User found";
        public const string PAGE_SIZE = "Invalid Entry";
        public const string PAGE_NUMBER = "Invalid Entry";
        public const string EMPTY_EMAIL_ERROR_MESSAGE = "Email is Required";
        public const string EMAIL_ERROR_MESSAGE = "Not Valid Email Address";
        public const string EMPTY_PASSWORD_ERROR_MESSAGE = "Password is Required";
        public const string PASSWORD_ERROR_MESSAGE = "Password must have at least 8 character, 1 lower, 1 upper and special symbol.";

        public const string USER_NOT_FOUND = "User not found";
        public const string PASSWORD_INCORRECT = "Password is Incorrect";

        public const string NOT_FOUND = "not found";

        public const string EMPTY_NAME_ERROR_MESSAGE = "name is required.";
        public const string INVALID_NAME_LENGTH_ERROR_MESSAGE = "name must be less than 100 characters.";
        public const string EMPTY_DURATION_ERROR_MESSAGE = "Duration is required.";
        public const string INVALID_DURATION_ERROR_MESSAGE = "Duration must be a positive number.";
    }
}
