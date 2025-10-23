using CarCompany.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.DTOs
{
    public class SalesUnitsPercentageByCenterResponse
    {
        public List<CenterData> CenterPercentages { get; set; }
    }

    public class CenterData
    {
        public string CenterName { get; set; }
        public Dictionary<TypeCar, ModelPercentageData> ModelPercentages { get; set; }
        public CenterData(string centerName, Dictionary<TypeCar, ModelPercentageData> modelPercentages)
        {
            CenterName = centerName;
            ModelPercentages = modelPercentages;
        }
    }

    public class ModelPercentageData
    {
        public int Units { get; set; }
        public decimal Percentage { get; set; }
    }
}
