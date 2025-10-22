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
    public class CreateSaleRequest
    {
        [Required(ErrorMessage = "Please select a distribution center")]
        public Guid DistributionCenterID { get; set; }


        [Required(ErrorMessage = "Please select a car type")]
        public TypeCar CarType { get; set; }

        /// <summary>
        /// Converts the current object of CreateSaleRequest into a new object of Sale
        /// </summary>
        /// <returns></returns>
        public Sale ToSale()
        {
            return new Sale()
            {
                Car = new Car(this.CarType),
                DistributionCenterID = this.DistributionCenterID,
            };
        }
    }
}
