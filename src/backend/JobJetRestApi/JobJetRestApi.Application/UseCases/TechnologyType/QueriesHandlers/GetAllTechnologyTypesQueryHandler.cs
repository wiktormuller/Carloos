using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.QueriesHandlers
{
    public class GetAllTechnologyTypesQueryHandler : IRequestHandler<GetAllTechnologyTypesQuery, List<TechnologyTypeResponse>>
    {
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        
        public GetAllTechnologyTypesQueryHandler(ITechnologyTypeRepository technologyTypeRepository)
        {
            _technologyTypeRepository = technologyTypeRepository;
        }

        public async Task<List<TechnologyTypeResponse>> Handle(GetAllTechnologyTypesQuery request, CancellationToken cancellationToken)
        {
            var technologyTypes = await _technologyTypeRepository.GetAll();

            return technologyTypes
                .Select(x => new TechnologyTypeResponse(x.Id, x.Name))
                .ToList();
        }
    }
}