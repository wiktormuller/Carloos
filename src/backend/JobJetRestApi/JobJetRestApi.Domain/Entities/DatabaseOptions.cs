namespace JobJetRestApi.Domain.Entities
{
    public class DatabaseOptions // @TODO - Where to put files like this?
    {
        public const string ConnectionStrings = "ConnectionStrings";
        
        public string DefaultConnection { get; set; }
    }
}