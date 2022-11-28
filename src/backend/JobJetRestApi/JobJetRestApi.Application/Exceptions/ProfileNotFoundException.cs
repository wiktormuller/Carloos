namespace JobJetRestApi.Application.Exceptions;

public class ProfileNotFoundException: System.Exception
{
    private ProfileNotFoundException(string message) : base(message) {}

    public static ProfileNotFoundException ForId(int id) =>
        new ProfileNotFoundException($"Profile with Id: #{id} not found.'");
}