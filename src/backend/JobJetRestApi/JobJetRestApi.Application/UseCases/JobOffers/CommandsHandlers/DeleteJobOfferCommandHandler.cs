using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class DeleteJobOfferCommandHandler : IRequestHandler<DeleteJobOfferCommand>
    {
        public Task<Unit> Handle(DeleteJobOfferCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}