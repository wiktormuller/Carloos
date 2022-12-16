using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.JobOfferApplications.Queries;

public interface IJobOfferApplicationQueries
{
    Task<JobOfferApplicationFileResponse> GetJobOfferApplicationByIdAsync(int jobOfferId, int jobOfferApplicationId, int currentUserId);
    Task<IEnumerable<JobOfferApplicationResponse>> GetAllJobOfferApplications(int jobOfferId, int currentUserId);
}