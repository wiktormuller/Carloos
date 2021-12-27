using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.QueriesHandlers
{
    public class GetTechnologyTypeByIdQueryHandler : IRequestHandler<GetTechnologyTypeByIdQuery, TechnologyTypeResponse>
    {
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        
        public GetTechnologyTypeByIdQueryHandler(ITechnologyTypeRepository technologyTypeRepository)
        {
            _technologyTypeRepository = technologyTypeRepository;
        }
        
        public async Task<TechnologyTypeResponse> Handle(GetTechnologyTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _technologyTypeRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }
            
            var technologyType = await _technologyTypeRepository.GetById(request.Id);

            var result = new TechnologyTypeResponse(technologyType.Id, technologyType.Name);

            return result;
        }
    }
}