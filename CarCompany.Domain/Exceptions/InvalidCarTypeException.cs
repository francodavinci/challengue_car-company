using CarCompany.Domain.Enums;

namespace CarCompany.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when an invalid car type is provided
    /// </summary>
    public class InvalidCarTypeException : DomainException
    {
        public InvalidCarTypeException(TypeCar carType) 
            : base($"Invalid car type: {carType}. Valid types are: {string.Join(", ", Enum.GetValues<TypeCar>())}") 
        {
            CarType = carType;
        }

        public TypeCar CarType { get; }
    }
}
