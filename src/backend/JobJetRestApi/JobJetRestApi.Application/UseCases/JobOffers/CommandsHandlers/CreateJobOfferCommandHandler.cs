using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, int>
    {
        public Task<int> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}