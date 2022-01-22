using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
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
            _jobJetDbContext = jobJetDbContext;
        }

        public async Task<JobOffer> GetById(int id)
        {
            return await _jobJetDbContext.JobOffers
                .Include(jobOffer => jobOffer.Address)
                .Include(jobOffer => jobOffer.Currency)
                .Include(jobOffer => jobOffer.Seniority)
                .Include(jobOffer => jobOffer.EmploymentType)
                .Include(jobOffer => jobOffer.TechnologyType)
                .FirstOrDefaultAsync(jobOffer => jobOffer.Id == id);
        }

        public async Task<List<JobOffer>> GetAll()
        {
            return await _jobJetDbContext.JobOffers
                .Include(jobOffer => jobOffer.Address.Country)
                .Include(jobOffer => jobOffer.Currency)
                .Include(jobOffer => jobOffer.Seniority)
                .Include(jobOffer => jobOffer.EmploymentType)
                .Include(jobOffer => jobOffer.TechnologyType)
                .ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await GetById(id) is not null;
        }

        public async Task<int> Create(JobOffer jobOffer)
        {
            await _jobJetDbContext.JobOffers.AddAsync(jobOffer);
            var jobOfferId = await _jobJetDbContext.SaveChangesAsync();

            return jobOffer.Id;
        }

        public async Task Update()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task Delete(JobOffer jobOffer)
        {
            _jobJetDbContext.Remove(jobOffer);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}