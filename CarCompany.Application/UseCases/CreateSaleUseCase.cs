using CarCompany.Application.DTOs;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Interfaces;
using CarCompany.Domain.Exceptions;
using CarCompany.Domain.Enums;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CarCompany.Application.UseCases
{
    public class CreateSaleUseCase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IDistributionCenterRepository _distributionCenterRepository;
        private readonly ILogger<CreateSaleUseCase> _logger;

        public CreateSaleUseCase(
            ISalesRepository salesRepository,
            IDistributionCenterRepository distributionCenterRepository,
            ILogger<CreateSaleUseCase> logger)
        {
            _salesRepository = salesRepository;
            _distributionCenterRepository = distributionCenterRepository;
            _logger = logger;
        }

        public SaleResponse Execute(SaleRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInformation("Starting CreateSaleUseCase execution for DistributionCenter: {DistributionCenterId}", 
                    request.DistributionCenterID);

                // Validate car type
                if (!Enum.IsDefined(typeof(TypeCar), request.CarType))
                {
                    throw new InvalidCarTypeException(request.CarType);
                }

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

                var result = savedSale.ToSaleResponse();
                
                stopwatch.Stop();
                _logger.LogInformation("CreateSaleUseCase completed successfully in {ElapsedMs}ms. Sale ID: {SaleId}", 
                    stopwatch.ElapsedMilliseconds, result.ID);

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "CreateSaleUseCase failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }
}