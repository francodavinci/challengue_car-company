using CarCompany.Application.DTOs;
using CarCompany.Application.RepositoriesContracts;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Enums;

namespace CarCompany.Infrastructure.Repositories
{
    public class SalesRepository : ISalesRepositoryContract
    {
        private readonly List<Sale> _sales = new()
        {
            // TOYOTA - BELLA VISTA
            new Sale ( new (TypeCar.SUV),  Guid.Parse("71EDF1A7-032E-4435-8DA5-C5A9E99B3026")),
            new Sale ( new (TypeCar.SEDAN) , Guid.Parse("71EDF1A7-032E-4435-8DA5-C5A9E99B3026")),
            new Sale ( new (TypeCar.SEDAN), Guid.Parse("71EDF1A7-032E-4435-8DA5-C5A9E99B3026")),
            new Sale ( new (TypeCar.SPORT), Guid.Parse("71EDF1A7-032E-4435-8DA5-C5A9E99B3026")),
            new Sale ( new (TypeCar.SPORT), Guid.Parse("71EDF1A7-032E-4435-8DA5-C5A9E99B3026")),
            
            // TOYOTA - LANUS
            new Sale ( new (TypeCar.SUV) , Guid.Parse("3A991B26-02AC-4926-ADE9-C0BE743A7556")),
            new Sale ( new (TypeCar.OFFROAD), Guid.Parse("3A991B26-02AC-4926-ADE9-C0BE743A7556")),
            new Sale ( new (TypeCar.OFFROAD), Guid.Parse("3A991B26-02AC-4926-ADE9-C0BE743A7556")),
            
            // TOYOTA - VILLA CRESPO
            new Sale ( new (TypeCar.SPORT), Guid.Parse("8234AC68-0294-4C9F-BE95-A10488E4B401")),
            new Sale ( new (TypeCar.SUV), Guid.Parse("8234AC68-0294-4C9F-BE95-A10488E4B401")),
            new Sale ( new (TypeCar.SEDAN), Guid.Parse("8234AC68-0294-4C9F-BE95-A10488E4B401")),
            
            // TOYOTA - OLAVARRIA
            new Sale ( new (TypeCar.SPORT), Guid.Parse("F61AECA3-7F02-48A7-864E-AD5B14155C94")),
            new Sale ( new (TypeCar.SPORT), Guid.Parse("F61AECA3-7F02-48A7-864E-AD5B14155C94")),
        };

        public Sale Add(Sale sale)
        {
            _sales.Add(sale);

            return sale;
        }

        public IEnumerable<Sale> GetAll() => _sales.ToList();

        public IEnumerable<Sale> GetByDistributionCenter(Guid? distributionCenterID) => _sales.Where(x => x.DistributionCenterID == distributionCenterID);

        public IEnumerable<Sale> GetByTypeModel(TypeCar model) =>_sales.Where(x => x.Car.Model == model);
    }
}
