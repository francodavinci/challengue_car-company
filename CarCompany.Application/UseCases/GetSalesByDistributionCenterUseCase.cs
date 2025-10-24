using CarCompany.Application.DTOs;
using CarCompany.Domain.Interfaces;
using CarCompany.Domain.Exceptions;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CarCompany.Application.UseCases
{
    public class GetSalesByDistributionCenterUseCase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IDistributionCenterRepository _distributionCenterRepository;
        private readonly ILogger<GetSalesByDistributionCenterUseCase> _logger;

        public GetSalesByDistributionCenterUseCase(
            ISalesRepository salesRepository,
            IDistributionCenterRepository distributionCenterRepository,
            ILogger<GetSalesByDistributionCenterUseCase> logger)
        {
            _salesRepository = salesRepository;
            _distributionCenterRepository = distributionCenterRepository;
            _logger = logger;
        }

        public SalesByDistributionCenterResponse Execute(Guid distributionCenterID)
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInformation("Starting GetSalesByDistributionCenterUseCase execution for DistributionCenter: {DistributionCenterId}", 
                    distributionCenterID);

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

                var result = new SalesByDistributionCenterResponse
                {
                    TotalAmount = totalAmount,
                    TotalUnits = totalUnits
                };

                stopwatch.Stop();
                _logger.LogInformation("GetSalesByDistributionCenterUseCase completed successfully in {ElapsedMs}ms. Total Amount: {TotalAmount}, Total Units: {TotalUnits}", 
                    stopwatch.ElapsedMilliseconds, result.TotalAmount, result.TotalUnits);

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "GetSalesByDistributionCenterUseCase failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }
}