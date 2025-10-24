using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarCompany.Domain.Interfaces;
using CarCompany.Application.DTOs;

namespace CarCompany.Application.UseCases
{
    public class GetUnitsSalesPercentageByDistributionCenter
    {

        private readonly ISalesRepository _salesRepository;
        private readonly IDistributionCenterRepository _distributionCenterRepository;

        public GetUnitsSalesPercentageByDistributionCenter(
            ISalesRepository salesRepository,
            IDistributionCenterRepository distributionCenterRepository)
        {
            _salesRepository = salesRepository;
            _distributionCenterRepository = distributionCenterRepository;
        }

        public SalesUnitsPercentageByCenterResponse GetUnitsSalesPercentageByCenter()
        {
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

            return new SalesUnitsPercentageByCenterResponse
            {
                CenterPercentages = centerPercentages
            };
        }
    }
}
