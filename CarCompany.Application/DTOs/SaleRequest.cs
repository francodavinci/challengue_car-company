using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CarCompany.Domain.Enums;
using System.Reflection;
using CarCompany.Domain.Entities;

namespace CarCompany.Application.DTOs
{
    /// <summary>
    /// Request to create a new automobile sale
    /// </summary>
    public class SaleRequest
    {
        /// <summary>
        /// ID of the distribution center where the sale was made
        /// </summary>
        [Required(ErrorMessage = "Please select a distribution center")]
        public Guid DistributionCenterID { get; set; }

        /// <summary>
        /// Type of automobile sold (0=SEDAN, 1=SUV, 2=OFFROAD)
        /// </summary>
        [Required(ErrorMessage = "Please select a car type")]
        public TypeCar CarType { get; set; }

        /// <summary>
        /// Converts the current object of CreateSaleRequest into a new object of Sale
        /// </summary>
        /// <returns></returns>
        public Sale ToSale()
        {
            Car car = new (this.CarType);
            Sale sale = new (car, this.DistributionCenterID);

            return sale;
        }
    }
}
