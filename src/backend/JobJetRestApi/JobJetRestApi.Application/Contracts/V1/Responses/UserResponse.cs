namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class UserResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public UserResponse(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}