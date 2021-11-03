using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.Commands
{
    public class DeleteJobOfferCommand : IRequest
    {
        public int Id { get; }

        public DeleteJobOfferCommand(int id)
        {
            Id = id;
        }
    }
}