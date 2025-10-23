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
        Sale Add(Sale sale);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Sale> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distributionCenterID"></param>
        /// <returns></returns>
        IEnumerable<Sale> GetByDistributionCenter(Guid? distributionCenterID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<Sale> GetByTypeModel(TypeCar model);
    }
}
