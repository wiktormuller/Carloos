namespace JobJetRestApi.Application.Exceptions
{
    public class IncorrectDelimiterOfCoordinatesException : System.Exception
    {
        private IncorrectDelimiterOfCoordinatesException(string message) : base(message) {}

        public static IncorrectDelimiterOfCoordinatesException ForCoordinates(string coordinates) =>
            new IncorrectDelimiterOfCoordinatesException($"Incorrect delimiter of coordinates exception for coordinates: '{coordinates}'.");
    }
}