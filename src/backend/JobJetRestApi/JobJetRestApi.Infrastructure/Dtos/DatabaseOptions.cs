namespace JobJetRestApi.Infrastructure.Dtos
{
    public class DatabaseOptions
    {
        public const string ConnectionStrings = "ConnectionStrings";
        
        public string DefaultConnection { get; set; }
    }
}