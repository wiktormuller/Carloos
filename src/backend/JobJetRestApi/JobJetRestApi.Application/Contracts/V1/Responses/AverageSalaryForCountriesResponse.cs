namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class AverageSalaryForCountriesResponse
{
    public int CountryId { get; private set; }
    public string CountryName { get; private set; }
    public decimal AverageSalaryFrom { get; private set; }
    public decimal AverageSalaryTo { get; private set; }
    public decimal AverageSalary { get; private set; }
    
    public AverageSalaryForCountriesResponse(int countryId, string countryName, 
        decimal averageSalaryFrom, decimal averageSalaryTo, decimal averageSalary)
    {
        CountryId = countryId;
        CountryName = countryName;
        AverageSalaryFrom = averageSalaryFrom;
        AverageSalaryTo = averageSalaryTo;
        AverageSalary = averageSalary;
    }
}