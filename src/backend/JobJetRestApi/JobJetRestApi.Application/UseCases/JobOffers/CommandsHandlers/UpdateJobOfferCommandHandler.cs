using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class UpdateJobOfferCommandHandler : IRequestHandler<UpdateJobOfferCommand>
    {
        public Task<Unit> Handle(UpdateJobOfferCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}