using System.Net;
using JobJetRestApi.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace JobJetRestApi.Domain.Tests.Integration.Companies;

public class CompaniesTests
{
    [Fact]
    public async Task GetById_IfIdExists_ShouldReturns200Success()
    {
        var application = GetWebApplication();
        var client = application.CreateClient();

        var response = await client.GetAsync("/api/v1/companies/1");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ShouldAlwaysReturns200Success()
    {
        var application = GetWebApplication();
        var client = application.CreateClient();

        var response = await client.GetAsync("/api/v1/companies");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Delete_WhenDidNotPassAccessToken_ShouldReturns401Unauthorized()
    {
        var application = GetWebApplication();
        var client = application.CreateClient();

        var response = await client.DeleteAsync("/api/v1/companies/100");
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    private WebApplicationFactory<Program> GetWebApplication()
        => new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
            });
            builder
                .ConfigureAppConfiguration(x => x.AddEnvironmentVariables(prefix: "JobJetVariables_"));
        });
}