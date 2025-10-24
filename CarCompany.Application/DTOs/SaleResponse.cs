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
    public class SaleResponse
    {
        public Guid ID { get; set; }

        public Car Car { get; set; }

        public Guid DistributionCenterID { get; set; }

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
