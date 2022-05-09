namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class AverageSalaryInCountryResponse
{
    public int CountryId { get; private set; }
    public decimal AverageSalary { get; private set; }
    public decimal MedianSalary { get; private set; }

    public AverageSalaryInCountryResponse(int countryId, decimal averageSalary, decimal medianSalary)
    {
        CountryId = countryId;
        AverageSalary = averageSalary;
        MedianSalary = medianSalary;
    }
}