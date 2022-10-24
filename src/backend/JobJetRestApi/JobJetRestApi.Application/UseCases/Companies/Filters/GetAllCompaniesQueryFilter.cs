namespace JobJetRestApi.Application.UseCases.Companies.Filters
{
    public class GetAllCompaniesQueryFilter
    {
        public string NameContains { get; private set; }
        public string ShortNameContains { get; private set; }
        public string DescriptionContains { get; private set; }
        public int? NumberOfPeopleEquals { get; private set; }
        public string CityNameContains { get; private set; }
    }
}