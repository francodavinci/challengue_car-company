using CarCompany.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.Interfaces
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
        SaleResponse CreateSale(SaleRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TotalSalesResponse GetTotalSalesVolume();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        SalesByDistributionCenterResponse GetSalesByDistributionCenter(Guid distributionCenterID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        SalesUnitsPercentageByCenterResponse GetUnitsSalesPercentageByCenter();
    }
}
