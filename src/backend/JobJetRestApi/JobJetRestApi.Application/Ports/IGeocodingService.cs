using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Ports
{
    public interface IGeocodingService
    {
        Task<AddressCoords> ConvertAddressIntoCoordsAsync(string address);
    }
}