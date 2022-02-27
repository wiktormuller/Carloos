using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        private User() : base()
        {
        }
        
        public User(string email, string username)
        {
            Email = email;
            UserName = username;
        }
        
        public void UpdateName(string name)
        {
            UserName = name;
        }
    }
}