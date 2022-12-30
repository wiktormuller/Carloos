using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Enums;
using JobJetRestApi.Domain.Repositories;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, int>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IGeocodingService _geocodingService;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        public CreateJobOfferCommandHandler(ISeniorityRepository seniorityRepository, 
            ITechnologyTypeRepository technologyTypeRepository, 
            IEmploymentTypeRepository employmentTypeRepository, 
            ICountryRepository countryRepository, 
            IGeocodingService geocodingService, 
            ICurrencyRepository currencyRepository, 
            IUserRepository userRepository, 
            ICompanyRepository companyRepository)
        {
            _userRepository = Guard.Against.Null(userRepository, nameof(userRepository));
            _companyRepository = Guard.Against.Null(companyRepository, nameof(companyRepository));
            _seniorityRepository = Guard.Against.Null(seniorityRepository, nameof(seniorityRepository));
            _technologyTypeRepository = Guard.Against.Null(technologyTypeRepository, nameof(technologyTypeRepository));
            _employmentTypeRepository = Guard.Against.Null(employmentTypeRepository, nameof(employmentTypeRepository));
            _countryRepository = Guard.Against.Null(countryRepository, nameof(countryRepository));
            _geocodingService = Guard.Against.Null(geocodingService, nameof(geocodingService));
            _currencyRepository = Guard.Against.Null(currencyRepository, nameof(currencyRepository));
        }

        /// <exception cref="SeniorityLevelNotFoundException"></exception>
        /// <exception cref="TechnologyTypeNotFoundException"></exception>
        /// <exception cref="EmploymentTypeNotFoundException"></exception>
        /// <exception cref="CountryNotFoundException"></exception>
        /// <exception cref="CurrencyNotFoundException"></exception>
        /// <exception cref="InvalidAddressException"></exception>
        public async Task<int> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company is null)
            {
                throw CompanyNotFoundException.ForId(request.CompanyId);
            }
            
            if(!Enum.TryParse<WorkSpecification>(request.WorkSpecification, true, out WorkSpecification workSpecification))
            {
                throw IncorrectWorkSpecificationException.ForName(request.WorkSpecification);
            }
            
            if (! await _seniorityRepository.ExistsAsync(request.SeniorityId))
            {
                throw SeniorityLevelNotFoundException.ForId(request.SeniorityId);
            }

            var technologyTypeExists = await _technologyTypeRepository.ExistsAsync(request.TechnologyTypeIds);
            if (! technologyTypeExists.Exists)
            {
                throw TechnologyTypeNotFoundException.ForIds(technologyTypeExists.NonExistingIds);
            }

            if (! await _employmentTypeRepository.ExistsAsync(request.EmploymentTypeId))
            {
                throw EmploymentTypeNotFoundException.ForId(request.EmploymentTypeId);
            }

            if (! await _countryRepository.ExistsAsync(request.CountryId))
            {
                throw CountryNotFoundException.ForId(request.CountryId);
            }

            if (! await _currencyRepository.ExistsAsync(request.CurrencyId))
            {
                throw CurrencyNotFoundException.ForId(request.CurrencyId);
            }

            var addressFromInput = request.Street + " " + request.ZipCode + " " + request.Town;

            var addressCoords = await _geocodingService.ConvertAddressIntoCoordsAsync(addressFromInput);
            if (addressCoords is null)
            {
                throw InvalidAddressException.Default(addressFromInput);
            }
            
            var country = await _countryRepository.GetByIdAsync(request.CountryId);
            var address = new Address(country, request.Town, request.Street, request.ZipCode, addressCoords.Latitude, addressCoords.Longitude);

            var technologyTypes = await _technologyTypeRepository.GetByIdsAsync(request.TechnologyTypeIds);

            var seniorityLevel = await _seniorityRepository.GetByIdAsync(request.SeniorityId);

            var employmentType = await _employmentTypeRepository.GetByIdAsync(request.EmploymentTypeId);

            var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);

            var jobOffer = new JobOffer(
                request.Name,
                request.Description,
                request.SalaryFrom,
                request.SalaryTo,
                address,
                technologyTypes,
                seniorityLevel,
                employmentType,
                currency,
                workSpecification
                );
            
            var user = await _userRepository.GetByIdAsync(request.UserId);
            user.AddJobOffer(company, jobOffer);
            
            await _userRepository.UpdateAsync(user);

            return jobOffer.Id;
        }
    }
}