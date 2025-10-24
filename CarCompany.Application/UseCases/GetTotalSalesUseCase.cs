using CarCompany.Application.DTOs;
using CarCompany.Domain.Interfaces;

namespace CarCompany.Application.UseCases
{
    public class GetTotalSalesUseCase
    {
        private readonly ISalesRepository _salesRepository;

        public GetTotalSalesUseCase(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public TotalSalesResponse Execute()
        {
            var sales = _salesRepository.GetAll();
            var totalSales = sales.Sum(sale => sale.Car.Price);
            var totalUnits = sales.Count();

            return new TotalSalesResponse
            {
                TotalSales = totalSales,
                TotalUnits = totalUnits
            };
        }
    }
}