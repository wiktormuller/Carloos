namespace JobJetRestApi.Domain.Exceptions;

public class IncorrectCoordsException : System.Exception
{
    private IncorrectCoordsException(string message) : base(message) {}

    public static IncorrectCoordsException ForLatitude() =>
        new IncorrectCoordsException($"Latitude should be inclusive value between -90 and 90.");
    
    public static IncorrectCoordsException ForLongitude() =>
        new IncorrectCoordsException($"Longitude should be inclusive value between -180 and 180.");
}