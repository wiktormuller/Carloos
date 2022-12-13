using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Domain.Repositories;

public interface IJobOfferApplicationRepository
{
    Task CreateAsync(JobOfferApplication jobOfferApplication);
}