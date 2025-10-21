using CarCompany.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.DTOs
{
    public class SalesByDistributionCenterResponse
    {
        public Guid DistributionCenterID { get; set; }

        public decimal TotalAmount { get; set; }

        public int TotalUnits { get; set; }
    }
}
