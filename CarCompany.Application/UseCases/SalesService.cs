using CarCompany.Application.DTOs;
using CarCompany.Application.Interfaces;
using CarCompany.Application.RepositoriesContracts;
using CarCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepositoryContract _salesRepositoryContract;
        private readonly IDistributionCenterRepositoryContract _distributionCenterRepositoryContract;

        public SalesService(
            ISalesRepositoryContract salesRepositoryContract, 
            IDistributionCenterRepositoryContract istributionCenterRepositoryContract)
        {
            _salesRepositoryContract = salesRepositoryContract;
            _distributionCenterRepositoryContract = istributionCenterRepositoryContract;
        }

        public SaleResponse CreateSale(SaleRequest request)
        {
            // validate distribution center exists
            var distributionCenter = _distributionCenterRepositoryContract.GetById(request.DistributionCenterID);
            if (distributionCenter == null)
            {
                throw new ArgumentNullException($"The distribution center with ID {request.DistributionCenterID} does not exists");
            }

            // create car entity
            var car = new Car(request.CarType);

            // create sale entity
            var sale = new Sale(car, request.DistributionCenterID);

            // insert sale into the repository
            var savedSale = _salesRepositoryContract.Add(sale);

            return savedSale.ToSaleResponse();
        }

        public SalesByDistributionCenterResponse GetSalesByDistributionCenter(Guid distributionCenterID)
        {
            // validate distribution center exists
            var distributionCenter = _distributionCenterRepositoryContract.GetById(distributionCenterID);
            if (distributionCenter == null)
            {
                throw new ArgumentNullException($"The distribution center with ID {distributionCenterID} does not exists");
            }

            // obtain sales for the specific distribution center
            var sales = _salesRepositoryContract.GetByDistributionCenter(distributionCenterID);

            // calculate total amount and units 
            var totalAmount = sales.Sum(sale => sale.Car.Price);
            var totalUnits = sales.Count();

            return new SalesByDistributionCenterResponse
                {
                    TotalAmount = totalAmount,
                    TotalUnits = totalUnits
                };
        }

        public TotalSalesResponse GetTotalSalesVolume()
        {
            // obtain sales
            var sales = _salesRepositoryContract.GetAll();

            // calculate units and total price
            var totalSales = sales.Sum(sale => sale.Car.Price);
            var totalUnits = sales.Count();

            return new TotalSalesResponse
            {
                TotalSales = totalSales,
                TotalUnits = totalUnits
            };
        }

        public SalesUnitsPercentageByCenterResponse GetUnitsSalesPercentageByCenter()
        {
            // obtain all sales
            var sales = _salesRepositoryContract.GetAll();
            
            // obtain all distribution centers
            var distributionCenters = _distributionCenterRepositoryContract.GetAll();

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

                    CenterData centerData = new (center.Name, modelPercentages);

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
