using CarCompany.Domain.Entities;
using CarCompany.Domain.Enums;

namespace CarCompany.Domain.Interfaces
{
    public interface ISalesRepository
    {
        Sale Add(Sale sale);
        IEnumerable<Sale> GetAll();
        IEnumerable<Sale> GetByDistributionCenter(Guid distributionCenterId);
        IEnumerable<Sale> GetByTypeModel(TypeCar model);
    }
}
