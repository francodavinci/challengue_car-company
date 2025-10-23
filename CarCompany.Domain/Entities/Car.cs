using CarCompany.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Domain.Entities
{
    public class Car
    {
        private const decimal SPORT_TAX_CAR = 0.07m;

        private readonly Dictionary<TypeCar, decimal> _basePrices = new()
        {
            { TypeCar.SEDAN, 8000m },
            { TypeCar.SUV, 9500m },
            { TypeCar.OFFROAD, 12500m },
            { TypeCar.SPORT, 18200m }
        };

        public Guid Id { get; private set; }
        public decimal Price { get; private set; }
        public TypeCar Model { get; private set; }

        public Car(TypeCar model)
        {
            var basePrice = _basePrices[model];

            Id = Guid.NewGuid();
            Price = ApplyTax(model, basePrice);
            Model = model;
        }

        private decimal ApplyTax(TypeCar model, decimal basePrice)
        {
            return model == TypeCar.SPORT ? basePrice * (1 + SPORT_TAX_CAR) : basePrice;
        }
    }
}
