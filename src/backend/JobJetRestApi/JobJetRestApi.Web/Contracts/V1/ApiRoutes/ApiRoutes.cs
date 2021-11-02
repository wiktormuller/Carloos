namespace JobJetRestApi.Web.Contracts.V1.ApiRoutes
{
    public class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;

        public static class JobOffers
        {
            public const string GetAll = Base + "/job-offers";
            public const string Get = Base + "/job-offers/{jobOfferId}";
            public const string Create = Base + "/job-offers";
            public const string Update = Base + "/job-offers/{jobOfferId}";
            public const string Delete = Base + "/job-offers";
        }

        /*
        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
        }
        */
    }
}