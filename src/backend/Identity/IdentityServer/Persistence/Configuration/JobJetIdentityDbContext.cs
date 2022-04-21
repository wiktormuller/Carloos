using IdentityServer.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Persistence.Configuration;

public class JobJetIdentityDbContext : IdentityDbContext<User, Role, int>
{
    public JobJetIdentityDbContext(DbContextOptions<JobJetIdentityDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)  
    {
        base.OnModelCreating(builder);  
    }
}