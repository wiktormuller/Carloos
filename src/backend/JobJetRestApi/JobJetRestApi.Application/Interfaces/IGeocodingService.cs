using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface IGeocodingService
    {
        Task<AddressCoords> ConvertAddressIntoCoords(string address);
    }
}