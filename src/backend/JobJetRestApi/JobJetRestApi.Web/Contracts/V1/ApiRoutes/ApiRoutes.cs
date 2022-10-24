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
            public const string Get = Base + "/job-offers/{id}";
            public const string Create = Base + "/job-offers";
            public const string Update = Base + "/job-offers/{id}";
            public const string Delete = Base + "/job-offers/{id}";
        }

        public static class TechnologyTypes
        {
            public const string GetAll = Base + "/technology-types";
            public const string Get = Base + "/technology-types/{id}";
            public const string Create = Base + "/technology-types";
            public const string Update = Base + "/technology-types/{id}";
            public const string Delete = Base + "/technology-types/{id}";
        }

        public static class Currencies
        {
            public const string GetAll = Base + "/currencies";
            public const string Get = Base + "/currencies/{id}";
            public const string Create = Base + "/currencies";
            public const string Update = Base + "/currencies/{id}";
            public const string Delete = Base + "/currencies/{id}";
        }

        public static class SeniorityLevels
        {
            public const string GetAll = Base + "/seniority-levels";
            public const string Get = Base + "/seniority-levels/{id}";
            public const string Create = Base + "/seniority-levels";
            public const string Update = Base + "/seniority-levels/{id}";
            public const string Delete = Base + "/seniority-levels/{id}";
        }

        public static class Countries
        {
            public const string GetAll = Base + "/countries";
            public const string Get = Base + "/countries/{id}";
            public const string Create = Base + "/countries";
            public const string Update = Base + "/countries/{id}";
            public const string Delete = Base + "/countries/{id}";
        }

        public static class EmploymentTypes
        {
            public const string GetAll = Base + "/employment-types";
            public const string Get = Base + "/employment-types/{id}";
            public const string Create = Base + "/employment-types";
            public const string Update = Base + "/employment-types/{id}";
            public const string Delete = Base + "/employment-types/{id}";
        }
        
        public static class Companies
        {
            public const string GetAll = Base + "/companies";
            public const string Get = Base + "/companies/{id}";
            public const string Create = Base + "/companies";
            public const string Update = Base + "/companies/{id}";
            public const string Delete = Base + "/companies/{id}";
        }

        public static class Users
        {
            public const string GetAll = Base + "/users";
            public const string Get = Base + "/users/{id}";
            public const string Create = Base + "/users";
            public const string Update = Base + "/users/{id}";
            public const string Delete = Base + "/users/{id}";
            public const string GetRefreshTokens = Base + "/users/{id}/refresh-tokens";
        }

        public static class Roles
        {
            public const string GetAll = Base + "/roles";
            public const string Get = Base + "/roles/{id}";
            public const string Create = Base + "/roles";
            public const string AssignToUser = Base + "/roles/{id}/users/{userId}";
        }

        public static class Roads
        {
            public const string Get = Base + "/roads/{coordinates}";
        }

        public static class Auth
        {
            public const string Identity = Base + "/auth/identity";
            public const string Login = Base + "/auth/login";
            public const string Register = Base + "/auth/register";
            public const string Refresh = Base + "/auth/refresh";
            public const string Revoke = Base + "/auth/revoke";
        }

        public static class Dashboards
        {
            public const string GetAverageSalaryInCountries = Base + "/dashboards/average-salaries";
        }
    }
}