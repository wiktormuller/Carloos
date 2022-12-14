using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;

namespace JobJetRestApi.Infrastructure.Repositories;

public class JobOfferApplicationRepository : IJobOfferApplicationRepository
{
    private readonly JobJetDbContext _jobJetDbContext;
    
    public JobOfferApplicationRepository(JobJetDbContext jobJetDbContext)
    {
        _jobJetDbContext = Guard.Against.Null(jobJetDbContext, nameof(jobJetDbContext));
    }
    
    public async Task CreateAsync(JobOfferApplication jobOfferApplication)
    {
        _jobJetDbContext.JobOfferApplications.Add(jobOfferApplication);
        await _jobJetDbContext.SaveChangesAsync();
    }
}