using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using MediatR;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Domain.Repositories;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class UpdateJobOfferCommandHandler : IRequestHandler<UpdateJobOfferCommand>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IUserRepository _userRepository;

        public UpdateJobOfferCommandHandler(IJobOfferRepository jobOfferRepository, 
            IUserRepository userRepository)
        {
            _userRepository = Guard.Against.Null(userRepository, nameof(userRepository));
            _jobOfferRepository = Guard.Against.Null(jobOfferRepository, nameof(jobOfferRepository));
        }

        /// <exception cref="JobOfferNotFoundException"></exception>
        public async Task<Unit> Handle(UpdateJobOfferCommand request, CancellationToken cancellationToken)
        {
            if (! await _jobOfferRepository.ExistsAsync(request.Id))
            {
                throw JobOfferNotFoundException.ForId(request.Id);
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);
            user.UpdateJobOffer(
                request.Id,
                request.Name,
                request.Description,
                request.SalaryFrom,
                request.SalaryTo);

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}