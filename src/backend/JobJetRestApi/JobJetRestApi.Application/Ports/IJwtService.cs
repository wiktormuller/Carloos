using System.Collections.Generic;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Ports;

public interface IJwtService
{ 
    string GenerateJwt(User user, IList<string> roles);
}