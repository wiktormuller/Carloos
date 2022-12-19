using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators;

public class GetRoadRequestValidator : AbstractValidator<GetRoadRequest>
{
    public GetRoadRequestValidator()
    {
        RuleFor(request => request.SourceLongitude)
            .InclusiveBetween(-180, 180);
        
        RuleFor(request => request.SourceLatitude)
            .InclusiveBetween(-90, 90);
        
        RuleFor(request => request.DestinationLongitude)
            .InclusiveBetween(-180, 180);
        
        RuleFor(request => request.DestinationLatitude)
            .InclusiveBetween(-90, 90);
    }
}