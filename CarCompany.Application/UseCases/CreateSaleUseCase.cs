using CarCompany.Application.DTOs;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Interfaces;
using CarCompany.Domain.Exceptions;

namespace CarCompany.Application.UseCases
{
    public class CreateSaleUseCase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IDistributionCenterRepository _distributionCenterRepository;

        public CreateSaleUseCase(
            ISalesRepository salesRepository,
            IDistributionCenterRepository distributionCenterRepository)
        {
            _salesRepository = salesRepository;
            _distributionCenterRepository = distributionCenterRepository;
        }

        public SaleResponse Execute(SaleRequest request)
        {
            // Validate distribution center exists
            var distributionCenter = _distributionCenterRepository.GetById(request.DistributionCenterID);
            if (distributionCenter == null)
            {
                throw new DistributionCenterNotFoundException(request.DistributionCenterID);
            }

            // Create car entity
            var car = new Car(request.CarType);

            // Create sale entity
            var sale = new Sale(car, request.DistributionCenterID);

            // Insert sale into the repository
            var savedSale = _salesRepository.Add(sale);

            return savedSale.ToSaleResponse();
        }
    }
}