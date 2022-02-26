using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public User(string email, string username)
        {
            base.Email = email;
            base.UserName = username;
        }
        
        public void UpdateName(string name)
        {
            UserName = name;
        }
    }
}