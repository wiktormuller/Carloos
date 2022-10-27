namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class AverageSalaryForSeniorityLevelsResponse
{
    public int SeniorityLevelId { get; private set; }
    public string SeniorityLevelName { get; private set; }
    public decimal AverageSalaryFrom { get; private set; }
    public decimal AverageSalaryTo { get; private set; }
    public decimal AverageSalary { get; private set; }
    
    public AverageSalaryForSeniorityLevelsResponse(int seniorityLevelId, string seniorityLevelName,
        decimal averageSalaryFrom, decimal averageSalaryTo, decimal averageSalary)
    {
        AverageSalaryFrom = averageSalaryFrom;
        AverageSalaryTo = averageSalaryTo;
        AverageSalary = averageSalary;
        SeniorityLevelId = seniorityLevelId;
        SeniorityLevelName = seniorityLevelName;
    }
}