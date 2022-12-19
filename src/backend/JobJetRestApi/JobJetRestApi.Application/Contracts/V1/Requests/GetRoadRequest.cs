using System.ComponentModel.DataAnnotations;

namespace JobJetRestApi.Application.Contracts.V1.Requests;

public class GetRoadRequest
{
    [Required]
    public decimal SourceLongitude { get; set; }
    
    [Required]
    public decimal SourceLatitude { get; set; }
    
    [Required]
    public decimal DestinationLongitude { get; set; }
    
    [Required]
    public decimal DestinationLatitude { get; set; }
}