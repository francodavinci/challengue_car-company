using CarCompany.Application.DTOs;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.RepositoriesContracts
{
    /// <summary>
    /// Repository sales interface
    /// </summary>
    public interface ISalesRepositoryContract
    {

        /// <summary>
        /// Adds a new person into the list of persons
        /// </summary>
        /// <param name="request"></param> Person to add 
        /// <returns>Returns the sale response</returns>
        Task<Sale> AddAsync(CreateSaleRequest? request);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Sale>> GetAllAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distributionCenterID"></param>
        /// <returns></returns>
        Task<IEnumerable<Sale>> GetByDistributionCenterAsync(Guid? distributionCenterID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IEnumerable<Sale>> GetByTypeModelAsync(TypeCar model);
    }
}
