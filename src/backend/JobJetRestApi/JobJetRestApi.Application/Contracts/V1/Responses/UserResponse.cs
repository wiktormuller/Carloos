namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class UserResponse
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }

        public UserResponse(int id, string userName, string email)
        {
            Id = id;
            UserName = userName;
            Email = email;
        }
    }
}