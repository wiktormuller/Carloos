using System;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Application.Validators.RequestsValidators;

public class UsernameAsPasswordValidator<TUser> : IPasswordValidator<TUser> 
    where TUser : User
{
    public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
    {
        if (string.Equals(user.UserName, password, StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityError
            {
                Code = "UsernameAsPassword",
                Description = "You cannot use your username as your password"
            }));
        }
        return Task.FromResult(IdentityResult.Success);
    }
}