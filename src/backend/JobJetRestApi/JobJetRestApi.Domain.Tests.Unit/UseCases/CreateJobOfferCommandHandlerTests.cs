using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Enums;
using JobJetRestApi.Domain.Repositories;
using Moq;

namespace JobJetRestApi.Domain.Tests.Unit.UseCases;

public class CreateJobOfferCommandHandlerTests
{
    private readonly Mock<ISeniorityRepository> seniorityRepositoryMock;
    private readonly Mock<ITechnologyTypeRepository> technologyTypeRepository;
    private readonly Mock<IEmploymentTypeRepository> employmentTypeRepository;
    private readonly Mock<ICountryRepository> countryRepository;
    private readonly Mock<IGeocodingService> geocodingService;
    private readonly Mock<ICurrencyRepository> currencyRepository;
    private readonly Mock<IUserRepository> userRepository;
    private readonly Mock<ICompanyRepository> companyRepository;

    public CreateJobOfferCommandHandlerTests()
    {
        seniorityRepositoryMock = new Mock<ISeniorityRepository>();
        technologyTypeRepository = new Mock<ITechnologyTypeRepository>();
        employmentTypeRepository = new Mock<IEmploymentTypeRepository>();
        countryRepository = new Mock<ICountryRepository>();
        geocodingService = new Mock<IGeocodingService>();
        currencyRepository = new Mock<ICurrencyRepository>();
        userRepository = new Mock<IUserRepository>();
        companyRepository = new Mock<ICompanyRepository>();
    }
    
    [Fact]
    public async Task CreateJobOffer_WhenPassCorrectData_ShouldUpdateUserOnceViaRepository()
    {
        // Arrange
        var command = new CreateJobOfferCommand(
            1,
            1,
            "SomeName",
            "SomeDescription",
            5000,
            10000,
            new List<int>() { 1, 2 },
            1,
            1,
            "Łódź",
            "Cyganka 51",
            "94-221",
            1,
            1,
            WorkSpecification.FullyRemote.ToString()
        );

        var company = new Company("SomeCompanyName", "SomeCompanyShortName",
            "SomeCompanyDescription", 100, "Łódź");

        companyRepository
            .Setup(x => x.GetByIdAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(company);

        seniorityRepositoryMock
            .Setup(x => x.ExistsAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(true);

        technologyTypeRepository
            .Setup(x => x.ExistsAsync(It.Is<List<int>>(x => x.ElementAt(0) == 1 && x.ElementAt(1) == 2)))
            .ReturnsAsync((true, new List<int>()));

        employmentTypeRepository
            .Setup(x => x.ExistsAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(true);

        countryRepository
            .Setup(x => x.ExistsAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(true);

        currencyRepository
            .Setup(x => x.ExistsAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(true);

        geocodingService
            .Setup(x => x.ConvertAddressIntoCoordsAsync(It.IsAny<string>()))
            .ReturnsAsync(new AddressCoords("SomeAddress", 54, 21));

        countryRepository
            .Setup(x => x.GetByIdAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(new Country("Poland", "alpha2Code", "alpha3Code", 1, 20, 20));

        technologyTypeRepository
            .Setup(x => x.GetByIdsAsync(It.Is<List<int>>(id => id.ElementAt(0) == 1 && id.ElementAt(1) == 2)))
            .ReturnsAsync(new List<TechnologyType>()
                { new TechnologyType("FirstTechnology"), new TechnologyType("SecondTechnology") });

        seniorityRepositoryMock
            .Setup(x => x.GetByIdAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(new Seniority("SomeSeniority"));

        employmentTypeRepository
            .Setup(x => x.GetByIdAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(new EmploymentType("SomeEmploymentType"));

        currencyRepository
            .Setup(x => x.GetByIdAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(new Currency("SomeCurrency", "SomeIsoCode", 1));

        var user = new User("SomeEmail", "SomeUserName");
        user.AddCompany(company); // User have to own company to add new job offer
        
        userRepository
            .Setup(x => x.GetByIdAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(user);

        var handler = new CreateJobOfferCommandHandler(
            seniorityRepositoryMock.Object,
            technologyTypeRepository.Object,
            employmentTypeRepository.Object,
            countryRepository.Object,
            geocodingService.Object,
            currencyRepository.Object,
            userRepository.Object,
            companyRepository.Object);

        // Act
        await handler.Handle(command, new CancellationToken());

        // Assert
        userRepository.Verify(mock => mock.UpdateAsync(It.Is<User>(user => 
            user.Companies.First().JobOffers.First().Name == "SomeName" &&
            user.Companies.First().JobOffers.First().Description == "SomeDescription" &&
            user.Companies.First().JobOffers.First().SalaryFrom == 5000 &&
            user.Companies.First().JobOffers.First().SalaryTo == 10000 &&
            user.Companies.First().JobOffers.First().TechnologyTypes.ElementAt(0).Name == "FirstTechnology" &&
            user.Companies.First().JobOffers.First().TechnologyTypes.ElementAt(1).Name == "SecondTechnology" &&
            user.Companies.First().JobOffers.First().Seniority.Name == "SomeSeniority" &&
            user.Companies.First().JobOffers.First().EmploymentType.Name == "SomeEmploymentType" &&
            user.Companies.First().JobOffers.First().Address.Town == "Łódź" &&
            user.Companies.First().JobOffers.First().Address.Street == "Cyganka 51" &&
            user.Companies.First().JobOffers.First().Address.ZipCode == "94-221" &&
            user.Companies.First().JobOffers.First().Address.Country.Name == "Poland" &&
            user.Companies.First().JobOffers.First().Address.Longitude == 54 &&
            user.Companies.First().JobOffers.First().Address.Latitude == 21 &&
            user.Companies.First().JobOffers.First().Currency.Name == "SomeCurrency")), 
            Times.Once);
    }
    
    // @TODO: Implement more tests
}