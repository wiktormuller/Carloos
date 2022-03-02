using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.QueriesHandlers
{
    public class GetEmploymentTypeByIdQueryHandler : IRequestHandler<GetEmploymentTypeByIdQuery, EmploymentTypeResponse>
    {
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        
        public GetEmploymentTypeByIdQueryHandler(IEmploymentTypeRepository employmentTypeRepository)
        {
            _employmentTypeRepository = Guard.Against.Null(employmentTypeRepository, nameof(employmentTypeRepository));
        }

        /// <exception cref="EmploymentTypeNotFoundException"></exception>
        public async Task<EmploymentTypeResponse> Handle(GetEmploymentTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (! await _employmentTypeRepository.ExistsAsync(request.Id))
            {
                throw EmploymentTypeNotFoundException.ForId(request.Id);
            }

            var employmentType = await _employmentTypeRepository.GetByIdAsync(request.Id);
            
            var result = new EmploymentTypeResponse(employmentType.Id, employmentType.Name);

            return result;
        }
    }
}