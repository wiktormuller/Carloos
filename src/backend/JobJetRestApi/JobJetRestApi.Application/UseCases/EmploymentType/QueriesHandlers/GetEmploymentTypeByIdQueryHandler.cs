using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.QueriesHandlers
{
    public class GetEmploymentTypeByIdQueryHandler : IRequestHandler<GetEmploymentTypeByIdQuery, EmploymentTypeResponse>
    {
        public GetEmploymentTypeByIdQueryHandler()
        {
            
        }

        public Task<EmploymentTypeResponse> Handle(GetEmploymentTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new EmploymentTypeResponse();

            return Task.FromResult(result);
        }
    }
}