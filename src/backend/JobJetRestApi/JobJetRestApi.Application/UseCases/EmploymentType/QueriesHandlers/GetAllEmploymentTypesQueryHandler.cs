using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.QueriesHandlers
{
    public class GetAllEmploymentTypesQueryHandler : IRequestHandler<GetAllEmploymentTypesQuery, List<EmploymentTypeResponse>>
    {
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        
        public GetAllEmploymentTypesQueryHandler(IEmploymentTypeRepository employmentTypeRepository)
        {
            _employmentTypeRepository = employmentTypeRepository;
        }

        public async Task<List<EmploymentTypeResponse>> Handle(GetAllEmploymentTypesQuery request, CancellationToken cancellationToken)
        {
            var employmentTypes = await _employmentTypeRepository.GetAll();

            return employmentTypes
                .Select(x => new EmploymentTypeResponse(x.Id, x.Name))
                .ToList();
        }
    }
}