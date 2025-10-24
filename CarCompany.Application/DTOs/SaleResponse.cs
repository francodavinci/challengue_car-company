using CarCompany.Domain.Entities;
using CarCompany.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.DTOs
{
    /// <summary>
    /// Response with information of the created sale
    /// </summary>
    public class SaleResponse
    {
        /// <summary>
        /// Unique sale ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Information of the sold automobile
        /// </summary>
        public Car Car { get; set; } = null!;

        /// <summary>
        /// ID of the distribution center where the sale was made
        /// </summary>
        public Guid DistributionCenterID { get; set; }

        /// <summary>
        /// Sale date and time
        /// </summary>
        public DateTime Date {  get; set; }
    }

    public static class SaleExtension
    {
        /// <summary>
        /// An extension method to convert an object of Sale class into SaleResponse class
        /// </summary>
        /// <param name="person">The Sale object to convert</param>
        /// /// <returns>Returns the converted SaleReponse object</returns>
        public static SaleResponse ToSaleResponse(this Sale sale)
        {
            return new SaleResponse()
            {
                ID = sale.Id,
                Car = sale.Car,
                DistributionCenterID = sale.DistributionCenterID,
                Date = DateTime.Now,
            };
        }
    }
}
