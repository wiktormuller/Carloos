namespace JobJetRestApi.Application.Exceptions
{
    public class UserAlreadyExistsException : System.Exception
    {
        private UserAlreadyExistsException(string message) : base(message) {}

        public static UserAlreadyExistsException ForName(string name) =>
            new UserAlreadyExistsException($"User with name: '{name}' already exists.");
        
        public static UserAlreadyExistsException ForEmail(string email) =>
            new UserAlreadyExistsException($"User with email: '{email}' already exists.");
    }
}