namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class RoleResponse
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public RoleResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}