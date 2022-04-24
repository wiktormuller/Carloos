namespace JobJetRestApi.Application.Exceptions
{
    public class UserNotFoundException : System.Exception
    {
        private UserNotFoundException(string message) : base(message) {}

        public static UserNotFoundException ForId(int id) =>
            new UserNotFoundException($"User with Id: #{id} not found.");
        
        public static UserNotFoundException ForEmail(string email) =>
            new UserNotFoundException($"User with Email: '{email}' not found.");
    }
}