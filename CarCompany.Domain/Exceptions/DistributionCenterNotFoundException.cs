using CarCompany.Domain.Exceptions;

namespace CarCompany.Domain.Exceptions
{
    public class DistributionCenterNotFoundException : DomainException
    {
        public DistributionCenterNotFoundException(Guid id) 
            : base($"Distribution center with ID {id} not found") { }
    }
}
