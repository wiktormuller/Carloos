using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.Queries
{
    public class GetEmploymentTypeByIdQuery : IRequest<EmploymentTypeResponse>
    {
        public int Id { get; private set; }
        
        public GetEmploymentTypeByIdQuery(int id)
        {
            Id = id;
        }
    }
}