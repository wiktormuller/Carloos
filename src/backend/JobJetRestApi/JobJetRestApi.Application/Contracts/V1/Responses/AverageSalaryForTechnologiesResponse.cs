namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class AverageSalaryForTechnologiesResponse
{
    public int TechnologyTypeId { get; private set; }
    public string TechnologyTypeName { get; private set; }
    public decimal AverageSalaryFrom { get; private set; }
    public decimal AverageSalaryTo { get; private set; }
    public decimal AverageSalary { get; private set; }

    public AverageSalaryForTechnologiesResponse(int technologyTypeId, string technologyTypeName, 
        decimal averageSalaryFrom, decimal averageSalaryTo,  decimal averageSalary)
    {
        TechnologyTypeId = technologyTypeId;
        TechnologyTypeName = technologyTypeName;
        AverageSalary = averageSalary;
        AverageSalaryFrom = averageSalaryFrom;
        AverageSalaryTo = averageSalaryTo;
    }
}