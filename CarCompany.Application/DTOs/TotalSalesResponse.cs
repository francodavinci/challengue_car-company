using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.DTOs
{
    /// <summary>
    /// Response with total sales summary
    /// </summary>
    public class TotalSalesResponse
    {
        /// <summary>
        /// Total sum of all sales in monetary value
        /// </summary>
        public decimal TotalSales { get; set; }

        /// <summary>
        /// Total number of units sold
        /// </summary>
        public int TotalUnits { get; set; }
    }
}
