using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        private Role() {}

        public Role(string roleName) : base(roleName)
        {
        }
    }
}