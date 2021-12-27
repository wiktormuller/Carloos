using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.QueriesHandlers
{
    public class GetEmploymentTypeByIdQueryHandler : IRequestHandler<GetEmploymentTypeByIdQuery, EmploymentTypeResponse>
    {
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        
        public GetEmploymentTypeByIdQueryHandler(IEmploymentTypeRepository employmentTypeRepository)
        {
            _employmentTypeRepository = employmentTypeRepository;
        }

        public async Task<EmploymentTypeResponse> Handle(GetEmploymentTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (! await _employmentTypeRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            var employmentType = await _employmentTypeRepository.GetById(request.Id);
            
            var result = new EmploymentTypeResponse(employmentType.Id, employmentType.Name);

            return result;
        }
    }
}