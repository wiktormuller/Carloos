using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Domain.Exceptions;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class DeleteJobOfferCommandHandler : IRequestHandler<DeleteJobOfferCommand>
    {
        private readonly IJobOfferRepository _jobOfferRepository;

        public DeleteJobOfferCommandHandler(IJobOfferRepository jobOfferRepository)
        {
            _jobOfferRepository = jobOfferRepository;
        }

        public async Task<Unit> Handle(DeleteJobOfferCommand request, CancellationToken cancellationToken)
        {
            if (! await _jobOfferRepository.Exists(request.Id))
            {
                throw JobOfferNotFoundException.ForId(request.Id);
            }

            await _jobOfferRepository.Delete(request.Id);

            return Unit.Value;
        }
    }
}