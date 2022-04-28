using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.JobOffers.Queries
{
    public interface IJobOfferQueries
    {
        Task<IEnumerable<JobOfferResponse>> GetAllJobOffersAsync(UsersFilter usersFilter);
        Task<JobOfferResponse> GetJobOfferByIdAsync(int id);
    }
}