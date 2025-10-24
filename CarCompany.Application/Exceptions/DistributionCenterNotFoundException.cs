using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Application.Exceptions
{
    public class DistributionCenterNotFoundException : DomainException
    {
        public DistributionCenterNotFoundException(Guid id)
            : base($"Distribution center with ID {id} not found") { }
    }
}
