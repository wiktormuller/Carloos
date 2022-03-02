using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.QueriesHandlers
{
    public class GetTechnologyTypeByIdQueryHandler : IRequestHandler<GetTechnologyTypeByIdQuery, TechnologyTypeResponse>
    {
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        
        public GetTechnologyTypeByIdQueryHandler(ITechnologyTypeRepository technologyTypeRepository)
        {
            _technologyTypeRepository = Guard.Against.Null(technologyTypeRepository, nameof(technologyTypeRepository));
        }
        
        /// <exception cref="TechnologyTypeNotFoundException"></exception>
        public async Task<TechnologyTypeResponse> Handle(GetTechnologyTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _technologyTypeRepository.ExistsAsync(request.Id))
            {
                throw TechnologyTypeNotFoundException.ForId(request.Id);
            }
            
            var technologyType = await _technologyTypeRepository.GetByIdAsync(request.Id);

            var result = new TechnologyTypeResponse(technologyType.Id, technologyType.Name);

            return result;
        }
    }
}