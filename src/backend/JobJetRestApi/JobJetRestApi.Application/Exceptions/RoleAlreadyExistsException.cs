namespace JobJetRestApi.Application.Exceptions;

public class RoleAlreadyExistsException : System.Exception
{
    private RoleAlreadyExistsException(string message) : base(message) {}

    public static RoleAlreadyExistsException ForName(string name) =>
        new RoleAlreadyExistsException($"Role with Name: '{name}' already exists.");
}