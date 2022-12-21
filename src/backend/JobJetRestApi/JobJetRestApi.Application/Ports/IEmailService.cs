using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Ports;

public interface IEmailService
{
    Task SendEmailAsync(Email email);
}