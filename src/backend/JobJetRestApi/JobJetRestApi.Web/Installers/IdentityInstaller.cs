using System;
using System.Text;
using System.Threading.Tasks;
using JobJetRestApi.Application.Validators.RequestsValidators;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Options;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JobJetRestApi.Web.Installers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>();

            // IdentityModelEventSource.ShowPII = true;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<JobJetDbContext>(
                options => options.UseSqlServer(connectionString));
            
            // For Identity
            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireDigit = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<JobJetDbContext>()
                .AddDefaultTokenProviders()
                .AddPasswordValidator<UsernameAsPasswordValidator<User>>();

            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("CEO_Only", policy => 
                        policy.RequireUserName("ceo@jobjet.com"));
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}