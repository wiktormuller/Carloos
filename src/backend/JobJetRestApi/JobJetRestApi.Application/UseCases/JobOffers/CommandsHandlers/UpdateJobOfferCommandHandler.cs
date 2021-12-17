using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Domain.Exceptions;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class UpdateJobOfferCommandHandler : IRequestHandler<UpdateJobOfferCommand>
    {
        private readonly IJobOfferRepository _jobOfferRepository;

        public UpdateJobOfferCommandHandler(IJobOfferRepository jobOfferRepository)
        {
            _jobOfferRepository = jobOfferRepository;
        }

        public Task<Unit> Handle(UpdateJobOfferCommand request, CancellationToken cancellationToken)
        {
            if (!_jobOfferRepository.Exists(request.Id))
            {
                throw JobOfferNotFoundException.ForId(request.Id);
            }

            var jobOffer = _jobOfferRepository.GetById(request.Id);
            jobOffer.UpdateBasicInformation(request.Name, request.Description, request.SalaryFrom, request.SalaryTo);

            _jobOfferRepository.Update();
            
            return Task.FromResult(Unit.Value);
        }
    }
}