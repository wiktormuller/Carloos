using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.Profiles.Queries;

public interface IProfileQueries
{
    Task<MyProfileResponse> GetMyProfile(int currentUserId);
}