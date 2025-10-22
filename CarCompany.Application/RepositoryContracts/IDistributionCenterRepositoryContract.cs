using CarCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.RepositoriesContracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDistributionCenterRepositoryContract
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DistributionCenter>> GetAllAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DistributionCenter> GetByIdAsync(Guid id);
    }
}
