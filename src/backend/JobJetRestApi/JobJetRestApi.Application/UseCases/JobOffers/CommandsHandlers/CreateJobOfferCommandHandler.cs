using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Exceptions;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, int>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly ISeniorityRepository _seniorityRepository;
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IGeocodingService _geocodingService;
        private readonly ICurrencyRepository _currencyRepository;

        public CreateJobOfferCommandHandler(IJobOfferRepository jobOfferRepository, 
            ISeniorityRepository seniorityRepository, 
            ITechnologyTypeRepository technologyTypeRepository, 
            IEmploymentTypeRepository employmentTypeRepository, 
            ICountryRepository countryRepository, 
            IGeocodingService geocodingService, 
            ICurrencyRepository currencyRepository)
        {
            _jobOfferRepository = jobOfferRepository;
            _seniorityRepository = seniorityRepository;
            _technologyTypeRepository = technologyTypeRepository;
            _employmentTypeRepository = employmentTypeRepository;
            _countryRepository = countryRepository;
            _geocodingService = geocodingService;
            _currencyRepository = currencyRepository;
        }

        public async Task<int> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
        {
            if (!_seniorityRepository.Exists(request.SeniorityId))
            {
                throw SeniorityLevelNotFoundException.ForId(request.SeniorityId);
            }

            if (!_technologyTypeRepository.Exists(request.TechnologyTypeId))
            {
                throw TechnologyTypeNotFoundException.ForId(request.TechnologyTypeId);
            }

            if (!_employmentTypeRepository.Exists(request.EmploymentTypeId))
            {
                throw EmploymentTypeNotFoundException.ForId(request.EmploymentTypeId);
            }

            if (!_countryRepository.Exists(request.CountryIsoId))
            {
                throw CountryNotFoundException.ForId(request.CountryIsoId);
            }

            if (!_currencyRepository.Exists(request.CurrencyId))
            {
                throw CurrencyNotFoundException.ForId(request.CurrencyId);
            }

            var addressFromInput = request.Street + " " + request.ZipCode + " " + request.Town;

            var addressCoords = await _geocodingService.ConvertAddressIntoCoords(addressFromInput);
            if (addressCoords is null)
            {
                throw InvalidAddressException.Default(addressFromInput);
            }
            
            var country = _countryRepository.GetById(request.CountryIsoId);
            var address = new Address(country, request.Town, request.Street, request.ZipCode, addressCoords.Latitude, addressCoords.Longitude);

            var technologyType = _technologyTypeRepository.GetById(request.TechnologyTypeId);

            var seniorityLevel = _seniorityRepository.GetById(request.SeniorityId);

            var employmentType = _employmentTypeRepository.GetById(request.EmploymentTypeId);

            var currency = _currencyRepository.GetById(request.CurrencyId);
            
            var jobOffer = new JobOffer(
                request.Name,
                request.Description,
                request.SalaryFrom,
                request.SalaryTo,
                address,
                technologyType,
                seniorityLevel,
                employmentType,
                currency
                );

            var jobOfferId = _jobOfferRepository.Create(jobOffer);

            return jobOfferId;
        }
    }
}