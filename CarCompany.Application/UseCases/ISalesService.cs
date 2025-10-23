using CarCompany.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISalesService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SaleResponse CreateSale(CreateSaleRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TotalSalesResponse GetTotalSalesVolume();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesByDistributionCenterResponse> GetSalesByDistributionCenter();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        SalesPercentageByTypeCarReponse GetSalesPercentageByTypeCar();
    }
}
