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

        public static class TechnologyTypes
        {
            public const string GetAll = Base + "/technolog-types";
            public const string Get = Base + "/technolog-types/{technologyTypeId}";
            public const string Create = Base + "/technolog-types";
            public const string Update = Base + "/technolog-types/{technologyTypeId}";
            public const string Delete = Base + "/technolog-types";
        }

        public static class Currencies
        {
            public const string GetAll = Base + "/currencies";
            public const string Get = Base + "/currencies/{currencyId}";
            public const string Create = Base + "/currencies";
            public const string Update = Base + "/currencies/{currencyId}";
            public const string Delete = Base + "/currencies";
        }

        public static class SeniorityLevels
        {
            public const string GetAll = Base + "/seniority-levels";
            public const string Get = Base + "/seniority-levels/{seniorityLevelId}";
            public const string Create = Base + "/seniority-levels";
            public const string Update = Base + "/seniority-levels/{seniorityLevelId}";
            public const string Delete = Base + "/seniority-levels";
        }

        public static class Countries
        {
            public const string GetAll = Base + "/countries";
            public const string Get = Base + "/countries/{countryId}";
            public const string Create = Base + "/countries";
            public const string Update = Base + "/countries/{countryId}";
            public const string Delete = Base + "/countries";
        }

        public static class EmploymentTypes
        {
            public const string GetAll = Base + "/employment-types";
            public const string Get = Base + "/employment-types/{employmentTypeId}";
            public const string Create = Base + "/employment-types";
            public const string Update = Base + "/employment-types/{employmentTypeId}";
            public const string Delete = Base + "/employment-types";
        }

        public static class Roads
        {
            public const string Get = Base + "/roads/{coordinates}";
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