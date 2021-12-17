using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface IEmploymentTypeRepository
    {
        public EmploymentType GetById(int id);
        public bool Exists(int id);
    }
}