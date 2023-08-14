using FluentAssertions;
using JobJetRestApi.Domain.Consts;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Exceptions;

namespace JobJetRestApi.Domain.Tests.Unit.Entities;

public class UserTests
{
    [Fact]
    public void CreateUser_WhenCorrectDataIsPassed_ShouldCreateUser()
    {
        // Arrange
        var someEmail = "some@email.com";
        var someUsername = "someUsername";

        // Act
        var user = new User(someEmail, someUsername);

        // Assert
        Assert.Equal("some@email.com", user.Email);
        Assert.Equal("someUsername", user.UserName);
    }

    [Fact]
    public void CreateUser_WhenPassNullEmail_ShouldThrowsException()
    {
        // Arrange
        var nullEmail = (string)null;
        var someUsername = "someUsername";

        // Act
        var action = () => new User(nullEmail, someUsername);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("email");
    }
    
    [Fact]
    public void CreateUser_WhenPassEmptyEmail_ShouldThrowsException()
    {
        // Arrange
        var emptyEmail = string.Empty;
        var someUsername = "someUsername";

        // Act
        var action = () => new User(emptyEmail, someUsername);

        // Assert
        action.Should().Throw<ArgumentException>().WithParameterName("email");
    }
    
    [Fact]
    public void CreateUser_WhenPassNullUsername_ShouldThrowsException()
    {
        // Arrange
        var someEmail = "some@email.com";
        var nullUsername = (string)null;

        // Act
        var action = () => new User(someEmail, nullUsername);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("username");
    }
    
    [Fact]
    public void CreateUser_WhenPassEmptyUsername_ShouldThrowsException()
    {
        // Arrange
        var someEmail = "some@email.com";
        var emptyUsername = string.Empty;

        // Act
        var action = () => new User(someEmail, emptyUsername);

        // Assert
        action.Should().Throw<ArgumentException>().WithParameterName("username");
    }
    
    [Fact]
    public void UpdateUsername_WhenPassNullUsername_ShouldThrowsException()
    {
        // Arrange
        var nullUsername = (string)null;
        var user = new User("some@email.com", "someUsername");

        // Act
        var action = () => user.UpdateUsername(nullUsername);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("username");
    }
    
    [Fact]
    public void UpdateUsername_WhenPassEmptyUsername_ShouldThrowsException()
    {
        // Arrange
        var emptyUsername = string.Empty;
        var user = new User("some@email.com", "someUsername");

        // Act
        var action = () => user.UpdateUsername(emptyUsername);

        // Assert
        action.Should().Throw<ArgumentException>().WithParameterName("username");
    }

    [Fact]
    public void AddCompany_WhenPassCorrectData_ThenUserShouldOwnsIt()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");

        // Act
        user.AddCompany(company);

        // Assert
        user.Companies.Should().HaveCount(count => count == 1);
        user.Companies.Should().NotBeNullOrEmpty();
        user.Companies.First().Should().BeOfType<Company>();
        user.Companies.First().Name.Should().Be("someName");
        user.Companies.First().ShortName.Should().Be("someShortName");
        user.Companies.First().Description.Should().Be("someDescription");
        user.Companies.First().NumberOfPeople.Should().Be(10);
        user.Companies.First().CityName.Should().Be("someCityName");
    }

    [Fact]
    public void AddCompany_WhenPassNullCompany_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var nullCompany = (Company)null;

        // Act
        var action = () => user.AddCompany(nullCompany);

        // Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("company");
    }

    [Fact]
    public void AddCompany_WhenPassCompanyWithNameThatAlreadyExists_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        
        var firstCompany = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        
        var secondCompany = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        
        user.AddCompany(firstCompany);

        // Act
        var action = () => user.AddCompany(secondCompany);
        
        // Assert
        action.Should()
            .Throw<UserCannotHaveCompaniesWithTheSameNamesException>()
            .WithMessage("User cannot have companies with the same name: 'someName'.");
    }

    [Fact]
    public void DeleteCompany_WhenPassCorrectData_ThenShouldDeleteCompanyFromUser()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        
        company.GetType().GetProperty("Id").SetValue(company, 1);

        user.AddCompany(company);
        
        // Act
        user.DeleteCompany(1);
        
        // Assert
        user.Companies.Should().HaveCount(count => count == 0);
    }
    
    [Fact]
    public void DeleteCompany_WhenPassCompanyIdAsZero_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");

        // Act
        var action = () => user.DeleteCompany(0);
        
        // Assert
        action.Should().Throw<ArgumentException>().WithParameterName("companyId");
    }
    
    [Fact]
    public void DeleteCompany_WhenPassCompanyIdAsNegative_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");

        // Act
        var action = () => user.DeleteCompany(-10);
        
        // Assert
        action.Should().Throw<ArgumentException>().WithParameterName("companyId");
    }
    
    [Fact]
    public void DeleteCompany_WhenTryToDeleteCompanyThatIsNotOwnerOf_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");

        // Act
        var action = () => user.DeleteCompany(5);
        
        // Assert
        action.Should()
            .Throw<CannotDeleteCompanyInformationException>()
            .WithMessage("Cannot delete company, because you are not owner of company with Id: #5.");
    }

    [Fact]
    public void UpdateCompanyInformation_WhenPassCorrectData_ThenShouldUpdateCompany()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");

        company.GetType().GetProperty("Id").SetValue(company, 1);
        
        user.AddCompany(company);
        
        // Act
        user.UpdateCompanyInformation(1, "someNewDescription", 20);
        
        // Assert
        user.Companies.First().Description.Should().Be("someNewDescription");
        user.Companies.First().NumberOfPeople.Should().Be(20);
        user.Companies.First().Id.Should().Be(1);
    }
    
    [Fact]
    public void UpdateCompanyInformation_WhenPassZeroAsCompanyId_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var zeroAsCompanyId = 0;

        // Act
        var action = () => user.UpdateCompanyInformation(zeroAsCompanyId, "someDescription", 10);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName("companyId");
    }
    
    [Fact]
    public void UpdateCompanyInformation_WhenPassNegativeCompanyId_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var negativeCompanyId = -2;

        // Act
        var action = () => user.UpdateCompanyInformation(negativeCompanyId, "someDescription", 10);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName("companyId");
    }
    
    [Fact]
    public void UpdateCompanyInformation_WhenPassNullDescription_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var nullDescription = (string)null;

        // Act
        var action = () => user.UpdateCompanyInformation(1, nullDescription, 10);
        
        // Assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("description");
    }
    
    [Fact]
    public void UpdateCompanyInformation_WhenPassEmptyDescription_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var emptyDescription = string.Empty;

        // Act
        var action = () => user.UpdateCompanyInformation(1, emptyDescription, 10);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName("description");
    }
    
    [Fact]
    public void UpdateCompanyInformation_WhenPassNegativeNumberOfPeople_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var negativeNumberOfPeople = -5;

        // Act
        var action = () => user.UpdateCompanyInformation(1, "someDescription", negativeNumberOfPeople);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName("numberOfPeople");
    }
    
    [Fact]
    public void UpdateCompanyInformation_WhenPassNumberOfPeopleAsZero_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var numberOfPeopleAsZero = 0;

        // Act
        var action = () => user.UpdateCompanyInformation(1, "someDescription", numberOfPeopleAsZero);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName("numberOfPeople");
    }
    
    [Fact]
    public void UpdateCompanyInformation_WhenTryToUpdateCompanyThatIsNotOwnerOf_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var companyId = 20;

        // Act
        var action = () => user.UpdateCompanyInformation(companyId, "someDescription", 10);
        
        // Assert
        action.Should()
            .Throw<CannotUpdateCompanyInformationException>()
            .WithMessage("Cannot update company information, because you are not owner of company with Id: #20.");
    }

    [Fact]
    public void AddJobOffer_WhenPassCorrectData_ThenUserShouldOwnsIt()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        
        company.GetType().GetProperty("Id").SetValue(company, 1);
        user.AddCompany(company);
        
        var jobOffer = new JobOffer(
            "someName",
            "someDescription",
            5000,
            10000,
            new Address(
                new Country("someName", 
                    "alpha2Code", 
                    "alpha3Code", 
                    10, 
                    50, 
                    -12),
                "someTown",
                "someStreet",
                "someZipCode",
                60,
                10),
            new List<TechnologyType>()
            {
                new TechnologyType("someTechnology")
            },
            new Seniority("someName"),
            new EmploymentType("someName"),
            new Currency("someName", "someIsoCode", 123),
            WorkSpecification.Hybrid
        );

        // Act
        user.AddJobOffer(company, jobOffer);

        // Assert
        user.Companies.First().JobOffers.Should().HaveCount(count => count == 1);
        user.Companies.First().JobOffers.First().Name.Should().Be("someName");
        user.Companies.First().JobOffers.First().Description.Should().Be("someDescription");
        user.Companies.First().JobOffers.First().SalaryFrom.Should().Be(5000);
        user.Companies.First().JobOffers.First().SalaryTo.Should().Be(10000);
        
        user.Companies.First().JobOffers.First().Address.Country.Name.Should().Be("someName");
        user.Companies.First().JobOffers.First().Address.Country.Alpha2Code.Should().Be("alpha2Code");
        user.Companies.First().JobOffers.First().Address.Country.Alpha3Code.Should().Be("alpha3Code");
        user.Companies.First().JobOffers.First().Address.Country.NumericCode.Should().Be(10);
        user.Companies.First().JobOffers.First().Address.Country.LatitudeOfCapital.Should().Be(50);
        user.Companies.First().JobOffers.First().Address.Country.LongitudeOfCapital.Should().Be(-12);
        
        user.Companies.First().JobOffers.First().Address.Town.Should().Be("someTown");
        user.Companies.First().JobOffers.First().Address.Street.Should().Be("someStreet");
        user.Companies.First().JobOffers.First().Address.ZipCode.Should().Be("someZipCode");
        user.Companies.First().JobOffers.First().Address.Latitude.Should().Be(60);
        user.Companies.First().JobOffers.First().Address.Longitude.Should().Be(10);

        user.Companies.First().JobOffers.First().TechnologyTypes.Should().HaveCount(count => count == 1);
        user.Companies.First().JobOffers.First().TechnologyTypes.First().Name.Should().Be("someTechnology");

        user.Companies.First().JobOffers.First().Seniority.Name.Should().Be("someName");
        user.Companies.First().JobOffers.First().EmploymentType.Name.Should().Be("someName");
        user.Companies.First().JobOffers.First().Currency.Name.Should().Be("someName");
        user.Companies.First().JobOffers.First().Currency.IsoCode.Should().Be("someIsoCode");
        user.Companies.First().JobOffers.First().Currency.IsoNumber.Should().Be(123);

        user.Companies.First().JobOffers.First().WorkSpecification.Should().Be(WorkSpecification.Hybrid);
    }
    
    [Fact]
    public void AddJobOffer_WhenPassNullCompany_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var nullCompany = (Company)null;
        
        var jobOffer = new JobOffer(
            "someName",
            "someDescription",
            5000,
            10000,
            new Address(
                new Country("someName", "alpha2Code", "alpha3Code", 10, 50, -12),
                "someTown",
                "someStreet",
                "someZipCode",
                60,
                10),
            new List<TechnologyType>()
            {
                new TechnologyType("someTechnology")
            },
            new Seniority("someName"),
            new EmploymentType("someName"),
            new Currency("someName", "someIsoCode", 123),
            WorkSpecification.Hybrid
        );

        // Act
        var action = () => user.AddJobOffer(nullCompany, jobOffer);

        // Assert
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("company");
    }
    
    [Fact]
    public void AddJobOffer_WhenPassNullJobOffer_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        
        company.GetType().GetProperty("Id").SetValue(company, 1);
        user.AddCompany(company);

        var nullJobOffer = (JobOffer)null;

        // Act
        var action = () => user.AddJobOffer(company, nullJobOffer);

        // Assert
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("jobOffer");
    }
    
    [Fact]
    public void AddJobOffer_WhenPassCompanyThatIsNotOwnerOf_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        
        company.GetType().GetProperty("Id").SetValue(company, 1);

        var jobOffer = new JobOffer(
            "someName",
            "someDescription",
            5000,
            10000,
            new Address(
                new Country("someName", "alpha2Code", "alpha3Code", 10, 50, -12),
                "someTown",
                "someStreet",
                "someZipCode",
                60,
                10),
            new List<TechnologyType>()
            {
                new TechnologyType("someTechnology")
            },
            new Seniority("someName"),
            new EmploymentType("someName"),
            new Currency("someName", "someIsoCode", 123),
            WorkSpecification.Hybrid
        );

        // Act
        var action = () => user.AddJobOffer(company, jobOffer);

        // Assert
        action.Should().Throw<CannotCreateJobOfferException>()
            .WithMessage("Cannot create job offer, because you are not owner of company with Id: #1.");
    }

    [Fact]
    public void DeleteJobOffer_WhenPassCorrectData_JobOfferShouldBeDeleted()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        company.GetType().GetProperty("Id").SetValue(company, 1);
        
        var jobOffer = new JobOffer(
            "someName",
            "someDescription",
            5000,
            10000,
            new Address(
                new Country("someName", "alpha2Code", "alpha3Code", 10, 50, -12),
                "someTown",
                "someStreet",
                "someZipCode",
                60,
                10),
            new List<TechnologyType>()
            {
                new TechnologyType("someTechnology")
            },
            new Seniority("someName"),
            new EmploymentType("someName"),
            new Currency("someName", "someIsoCode", 123),
            WorkSpecification.Hybrid
        );
        jobOffer.GetType().GetProperty("Id").SetValue(jobOffer, 1);
        
        user.AddCompany(company);
        user.AddJobOffer(company, jobOffer);

        // Act
        user.DeleteJobOffer(1);
        
        // Assert
        user.Companies.First().JobOffers.Should().HaveCount(count => count == 0);
    }
    
    [Fact]
    public void DeleteJobOffer_WhenPassNegativeJobOfferId_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        company.GetType().GetProperty("Id").SetValue(company, 1);
        
        var jobOffer = new JobOffer(
            "someName",
            "someDescription",
            5000,
            10000,
            new Address(
                new Country("someName", "alpha2Code", "alpha3Code", 10, 50, -12),
                "someTown",
                "someStreet",
                "someZipCode",
                60,
                10),
            new List<TechnologyType>()
            {
                new TechnologyType("someTechnology")
            },
            new Seniority("someName"),
            new EmploymentType("someName"),
            new Currency("someName", "someIsoCode", 123),
            WorkSpecification.Hybrid
        );
        jobOffer.GetType().GetProperty("Id").SetValue(jobOffer, 1);
        
        user.AddCompany(company);
        user.AddJobOffer(company, jobOffer);

        var negativeJobOfferId = -10;

        // Act
        var action = () => user.DeleteJobOffer(negativeJobOfferId);
        
        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("jobOfferId");
    }
    
    [Fact]
    public void DeleteJobOffer_WhenPassJobOfferIdAsZero_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        company.GetType().GetProperty("Id").SetValue(company, 1);
        
        var jobOffer = new JobOffer(
            "someName",
            "someDescription",
            5000,
            10000,
            new Address(
                new Country("someName", "alpha2Code", "alpha3Code", 10, 50, -12),
                "someTown",
                "someStreet",
                "someZipCode",
                60,
                10),
            new List<TechnologyType>()
            {
                new TechnologyType("someTechnology")
            },
            new Seniority("someName"),
            new EmploymentType("someName"),
            new Currency("someName", "someIsoCode", 123),
            WorkSpecification.Hybrid
        );
        jobOffer.GetType().GetProperty("Id").SetValue(jobOffer, 1);
        
        user.AddCompany(company);
        user.AddJobOffer(company, jobOffer);

        var jobOfferIdAsZero = 0;

        // Act
        var action = () => user.DeleteJobOffer(jobOfferIdAsZero);
        
        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("jobOfferId");
    }
    
    [Fact]
    public void DeleteJobOffer_WhenPassJobOfferIdThatIsNotOwnerOf_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        company.GetType().GetProperty("Id").SetValue(company, 1);
        
        var jobOffer = new JobOffer(
            "someName",
            "someDescription",
            5000,
            10000,
            new Address(
                new Country("someName", "alpha2Code", "alpha3Code", 10, 50, -12),
                "someTown",
                "someStreet",
                "someZipCode",
                60,
                10),
            new List<TechnologyType>()
            {
                new TechnologyType("someTechnology")
            },
            new Seniority("someName"),
            new EmploymentType("someName"),
            new Currency("someName", "someIsoCode", 123),
            WorkSpecification.Hybrid
        );
        jobOffer.GetType().GetProperty("Id").SetValue(jobOffer, 1);
        
        user.AddCompany(company);
        user.AddJobOffer(company, jobOffer);

        var jobOfferThatDontOwn = 100;

        // Act
        var action = () => user.DeleteJobOffer(jobOfferThatDontOwn);
        
        // Assert
        action.Should().Throw<CannotDeleteJobOfferException>()
            .WithMessage("Cannot delete job offer, because you are not owner of job offer with Id: #100.");
    }

    [Fact]
    public void UpdateJobOffer_WhenPassCorrectData_ThenJobOfferShouldBeUpdated()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        
        var company = new Company("someName", "someShortName", 
            "someDescription", 10, "someCityName");
        company.GetType().GetProperty("Id").SetValue(company, 1);
        
        var jobOffer = new JobOffer(
            "someName",
            "someDescription",
            5000,
            10000,
            new Address(
                new Country("someName", "alpha2Code", "alpha3Code", 10, 50, -12),
                "someTown",
                "someStreet",
                "someZipCode",
                60,
                10),
            new List<TechnologyType>()
            {
                new TechnologyType("someTechnology")
            },
            new Seniority("someName"),
            new EmploymentType("someName"),
            new Currency("someName", "someIsoCode", 123),
            WorkSpecification.Hybrid
        );
        jobOffer.GetType().GetProperty("Id").SetValue(jobOffer, 1);
        
        user.AddCompany(company);
        user.AddJobOffer(company, jobOffer);
        
        // Act
        user.UpdateJobOffer(1, "newName", "newDescription", 6000, 11000);
        
        // Assert
        user.Companies.First().JobOffers.Should().HaveCount(count => count == 1);
        user.Companies.First().JobOffers.First().Name.Should().Be("newName");
        user.Companies.First().JobOffers.First().Description.Should().Be("newDescription");
        user.Companies.First().JobOffers.First().SalaryFrom.Should().Be(6000);
        user.Companies.First().JobOffers.First().SalaryTo.Should().Be(11000);
        
        user.Companies.First().JobOffers.First().Address.Country.Name.Should().Be("someName");
        user.Companies.First().JobOffers.First().Address.Country.Alpha2Code.Should().Be("alpha2Code");
        user.Companies.First().JobOffers.First().Address.Country.Alpha3Code.Should().Be("alpha3Code");
        user.Companies.First().JobOffers.First().Address.Country.NumericCode.Should().Be(10);
        user.Companies.First().JobOffers.First().Address.Country.LatitudeOfCapital.Should().Be(50);
        user.Companies.First().JobOffers.First().Address.Country.LongitudeOfCapital.Should().Be(-12);
        
        user.Companies.First().JobOffers.First().Address.Town.Should().Be("someTown");
        user.Companies.First().JobOffers.First().Address.Street.Should().Be("someStreet");
        user.Companies.First().JobOffers.First().Address.ZipCode.Should().Be("someZipCode");
        user.Companies.First().JobOffers.First().Address.Latitude.Should().Be(60);
        user.Companies.First().JobOffers.First().Address.Longitude.Should().Be(10);

        user.Companies.First().JobOffers.First().TechnologyTypes.Should().HaveCount(count => count == 1);
        user.Companies.First().JobOffers.First().TechnologyTypes.First().Name.Should().Be("someTechnology");

        user.Companies.First().JobOffers.First().Seniority.Name.Should().Be("someName");
        user.Companies.First().JobOffers.First().EmploymentType.Name.Should().Be("someName");
        user.Companies.First().JobOffers.First().Currency.Name.Should().Be("someName");
        user.Companies.First().JobOffers.First().Currency.IsoCode.Should().Be("someIsoCode");
        user.Companies.First().JobOffers.First().Currency.IsoNumber.Should().Be(123);

        user.Companies.First().JobOffers.First().WorkSpecification.Should().Be(WorkSpecification.Hybrid);
    }

    [Fact]
    public void UpdateJobOffer_WhenPassNegativeJobOfferId_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var negativeJobOfferId = -100;

        // Act
        var action = () => user.UpdateJobOffer(negativeJobOfferId, "someName", "someDescription", 5000, 10000);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("jobOfferId");
    }
    
    [Fact]
    public void UpdateJobOffer_WhenPassJobOfferIdAsZero_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var jobOfferIdAsZero = 0;

        // Act
        var action = () => user.UpdateJobOffer(jobOfferIdAsZero, "someName", "someDescription", 5000, 10000);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("jobOfferId");
    }
    
    [Fact]
    public void UpdateJobOffer_WhenPassNullName_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var nullName = (string)null;

        // Act
        var action = () => user.UpdateJobOffer(1, nullName, "someDescription", 5000, 10000);

        // Assert
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("name");
    }
    
    [Fact]
    public void UpdateJobOffer_WhenPassEmptyName_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var emptyName = string.Empty;

        // Act
        var action = () => user.UpdateJobOffer(1, emptyName, "someDescription", 5000, 10000);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("name");
    }
    
    [Fact]
    public void UpdateJobOffer_WhenPassNullDescription_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var nullDescription = (string)null;

        // Act
        var action = () => user.UpdateJobOffer(1, "someName", nullDescription, 5000, 10000);

        // Assert
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("description");
    }
    
    [Fact]
    public void UpdateJobOffer_WhenPassEmptyDescription_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var emptyDescription = string.Empty;

        // Act
        var action = () => user.UpdateJobOffer(1, "someName", emptyDescription, 5000, 10000);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("description");
    }
    
    [Fact]
    public void UpdateJobOffer_WhenPassNegativeSalaryFrom_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var negativeSalaryFrom = -100;

        // Act
        var action = () => user.UpdateJobOffer(1, "someName", "someDescription", negativeSalaryFrom, 10000);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("salaryFrom");
    }
    
    [Fact]
    public void UpdateJobOffer_WhenPassSalaryFromAsZero_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var salaryFromAsZero = 0;

        // Act
        var action = () => user.UpdateJobOffer(1, "someName", "someDescription", salaryFromAsZero, 10000);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("salaryFrom");
    }
    
    [Fact]
    public void UpdateJobOffer_WhenPassNegativeSalaryTo_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var negativeSalaryTo = -100;

        // Act
        var action = () => user.UpdateJobOffer(1, "someName", "someDescription", 5000, negativeSalaryTo);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("salaryTo");
    }
    
    [Fact]
    public void UpdateJobOffer_WhenPassSalaryToAsZero_ThenShouldThrowException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var salaryToAsZero = 0;

        // Act
        var action = () => user.UpdateJobOffer(1, "someName", "someDescription", 5000, salaryToAsZero);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithParameterName("salaryTo");
    }

    [Fact]
    public void UpdateJobOffer_WhenPassJobOfferIdThatIsNotOwner_ThenShouldThrowsException()
    {
        // Arrange
        var user = new User("some@email.com", "someUsername");
        var nonExistingJobOfferId = 100;
        
        // Act
        var action = () => user.UpdateJobOffer(nonExistingJobOfferId, "someName", "description", 5000, 10000);
        
        // Assert
        action.Should().Throw<CannotUpdateJobOfferException>()
            .WithMessage("Cannot update job offer, because you are not owner of job offer with Id: #100.");
    }
}