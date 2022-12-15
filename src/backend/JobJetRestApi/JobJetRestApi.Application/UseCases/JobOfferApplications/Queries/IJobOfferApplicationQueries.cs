using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.JobOfferApplications.Queries;

public interface IJobOfferApplicationQueries
{
    Task<JobOfferApplicationResponse> GetJobOfferApplicationByIdAsync(int jobOfferId, int jobOfferApplicationId, int currentUserId);
}