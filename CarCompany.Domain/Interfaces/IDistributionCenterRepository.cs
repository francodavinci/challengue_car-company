using CarCompany.Domain.Entities;

namespace CarCompany.Domain.Interfaces
{
    public interface IDistributionCenterRepository
    {
        IEnumerable<DistributionCenter> GetAll();
        DistributionCenter GetById(Guid id);
    }
}
