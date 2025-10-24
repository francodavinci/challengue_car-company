using CarCompany.Application.DTOs;
using CarCompany.Domain.Interfaces;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CarCompany.Application.UseCases
{
    public class GetTotalSalesUseCase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly ILogger<GetTotalSalesUseCase> _logger;

        public GetTotalSalesUseCase(ISalesRepository salesRepository, ILogger<GetTotalSalesUseCase> logger)
        {
            _salesRepository = salesRepository;
            _logger = logger;
        }

        public TotalSalesResponse Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInformation("Starting GetTotalSalesUseCase execution");

                var sales = _salesRepository.GetAll();
                var totalSales = sales.Sum(sale => sale.Car.Price);
                var totalUnits = sales.Count();

                var result = new TotalSalesResponse
                {
                    TotalSales = totalSales,
                    TotalUnits = totalUnits
                };

                stopwatch.Stop();
                _logger.LogInformation("GetTotalSalesUseCase completed successfully in {ElapsedMs}ms. Total Sales: {TotalSales}, Total Units: {TotalUnits}", 
                    stopwatch.ElapsedMilliseconds, result.TotalSales, result.TotalUnits);

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "GetTotalSalesUseCase failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }
}