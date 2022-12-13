using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Domain.Repositories;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class DeleteJobOfferCommandHandler : IRequestHandler<DeleteJobOfferCommand>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IUserRepository _userRepository;

        public DeleteJobOfferCommandHandler(IJobOfferRepository jobOfferRepository, 
            IUserRepository userRepository)
        {
            _userRepository = Guard.Against.Null(userRepository, nameof(userRepository));
            _jobOfferRepository = Guard.Against.Null(jobOfferRepository, nameof(jobOfferRepository));
        }

        public async Task<Unit> Handle(DeleteJobOfferCommand request, CancellationToken cancellationToken)
        {
            if (! await _jobOfferRepository.ExistsAsync(request.Id))
            {
                throw JobOfferNotFoundException.ForId(request.Id);
            }

            var user = await _userRepository.GetByIdAsync(request.CurrentUserId);
            user.DeleteJobOffer(request.Id);

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}