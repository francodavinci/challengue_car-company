using CarCompany.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.DTOs
{
    public class SalesPercentageByTypeCarReponse
    {
        public Guid Id { get; set; }

        public Dictionary<TypeCar, PercentageData> TypeCarPercentages { get; set; }

    }

    public class PercentageData { 
        public int Units { get; set; }
        public decimal Percentage { get; set; }
    }
}
