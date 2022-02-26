using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
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

            var jobOffer = await _jobOfferRepository.GetById(request.Id);
            await _jobOfferRepository.Delete(jobOffer);

            return Unit.Value;
        }
    }
}