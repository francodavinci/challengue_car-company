using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarCompany.Domain.Interfaces;
using CarCompany.Application.DTOs;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CarCompany.Application.UseCases
{
    public class GetUnitsSalesPercentageByDistributionCenter
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IDistributionCenterRepository _distributionCenterRepository;
        private readonly ILogger<GetUnitsSalesPercentageByDistributionCenter> _logger;

        public GetUnitsSalesPercentageByDistributionCenter(
            ISalesRepository salesRepository,
            IDistributionCenterRepository distributionCenterRepository,
            ILogger<GetUnitsSalesPercentageByDistributionCenter> logger)
        {
            _salesRepository = salesRepository;
            _distributionCenterRepository = distributionCenterRepository;
            _logger = logger;
        }

        public SalesUnitsPercentageByCenterResponse GetUnitsSalesPercentageByCenter()
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInformation("Starting GetUnitsSalesPercentageByDistributionCenter execution");

                // obtain all sales
                var sales = _salesRepository.GetAll();

                // obtain all distribution centers
                var distributionCenters = _distributionCenterRepository.GetAll();

                // calculate total units
                var totalUnits = sales.Count();

                // group sales by distribution center and then by car model
                var centerPercentages = new List<CenterData>();

                foreach (var center in distributionCenters)
                {
                    var centerSales = sales.Where(sale => sale.DistributionCenterID == center.Id);

                    if (centerSales.Any())
                    {
                        var modelPercentages = centerSales
                            .GroupBy(sale => sale.Car.Model)
                            .ToDictionary(
                                group => group.Key,
                                group => new ModelPercentageData
                                {
                                    Units = group.Count(),
                                    Percentage = totalUnits > 0 ? Math.Round((decimal)group.Count() / totalUnits * 100, 2) : 0
                                }
                            );

                        CenterData centerData = new(center.Name, modelPercentages);

                        centerPercentages.Add(centerData);
                    }
                }

                var result = new SalesUnitsPercentageByCenterResponse
                {
                    CenterPercentages = centerPercentages
                };

                stopwatch.Stop();
                _logger.LogInformation("GetUnitsSalesPercentageByDistributionCenter completed successfully in {ElapsedMs}ms. Centers processed: {CenterCount}", 
                    stopwatch.ElapsedMilliseconds, centerPercentages.Count);

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "GetUnitsSalesPercentageByDistributionCenter failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }
}
