using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Repositories
{
    public interface IJobOfferRepository
    {
        Task<JobOffer> GetByIdAsync(int id);
        Task<List<JobOffer>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
        Task<int> CreateAsync(JobOffer jobOffer);
        Task UpdateAsync();
        Task DeleteAsync(JobOffer jobOffer);
    }
}