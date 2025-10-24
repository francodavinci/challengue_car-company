using CarCompany.Application.DTOs;
using CarCompany.Domain.Interfaces;
using CarCompany.Domain.Exceptions;

namespace CarCompany.Application.UseCases
{
    public class GetSalesByDistributionCenterUseCase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IDistributionCenterRepository _distributionCenterRepository;

        public GetSalesByDistributionCenterUseCase(
            ISalesRepository salesRepository,
            IDistributionCenterRepository distributionCenterRepository)
        {
            _salesRepository = salesRepository;
            _distributionCenterRepository = distributionCenterRepository;
        }

        public SalesByDistributionCenterResponse Execute(Guid distributionCenterID)
        {
            // Validate distribution center exists
            var distributionCenter = _distributionCenterRepository.GetById(distributionCenterID);
            if (distributionCenter == null)
            {
                throw new DistributionCenterNotFoundException(distributionCenterID);
            }

            // Obtain sales for the specific distribution center
            var sales = _salesRepository.GetByDistributionCenter(distributionCenterID);

            // Calculate total amount and units
            var totalAmount = sales.Sum(sale => sale.Car.Price);
            var totalUnits = sales.Count();

            return new SalesByDistributionCenterResponse
            {
                TotalAmount = totalAmount,
                TotalUnits = totalUnits
            };
        }
    }
}