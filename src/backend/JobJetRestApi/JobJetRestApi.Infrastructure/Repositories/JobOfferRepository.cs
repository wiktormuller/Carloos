using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class JobOfferRepository : IJobOfferRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public JobOfferRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = Guard.Against.Null(jobJetDbContext, nameof(jobJetDbContext));
        }

        public async Task<JobOffer> GetByIdAsync(int id)
        {
            return await _jobJetDbContext.JobOffers
                .Include(jobOffer => jobOffer.Address)
                .Include(jobOffer => jobOffer.Currency)
                .Include(jobOffer => jobOffer.Seniority)
                .Include(jobOffer => jobOffer.EmploymentType)
                .Include(jobOffer => jobOffer.TechnologyTypes)
                .FirstOrDefaultAsync(jobOffer => jobOffer.Id == id);
        }

        public async Task<List<JobOffer>> GetAllAsync()
        {
            return await _jobJetDbContext.JobOffers
                .Include(jobOffer => jobOffer.Address.Country)
                .Include(jobOffer => jobOffer.Currency)
                .Include(jobOffer => jobOffer.Seniority)
                .Include(jobOffer => jobOffer.EmploymentType)
                .Include(jobOffer => jobOffer.TechnologyTypes)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await GetByIdAsync(id) is not null;
        }

        public async Task<int> CreateAsync(JobOffer jobOffer)
        {
            await _jobJetDbContext.JobOffers.AddAsync(jobOffer);
            var jobOfferId = await _jobJetDbContext.SaveChangesAsync();

            return jobOffer.Id;
        }

        public async Task UpdateAsync()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(JobOffer jobOffer)
        {
            _jobJetDbContext.Remove(jobOffer);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}