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
        IEnumerable<DistributionCenter> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DistributionCenter GetById(Guid id);
    }
}
