using CarCompany.Application.DTOs;
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

        public SaleResponse CreateSale(CreateSaleRequest request)
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

        public IEnumerable<SalesByDistributionCenterResponse> GetSalesByDistributionCenter()
        {
            throw new NotImplementedException();
        }

        public SalesPercentageByTypeCarReponse GetSalesPercentageByTypeCar()
        {
            throw new NotImplementedException();
        }

        public TotalSalesResponse GetTotalSalesVolume()
        {
            throw new NotImplementedException();
        }
    }
}
