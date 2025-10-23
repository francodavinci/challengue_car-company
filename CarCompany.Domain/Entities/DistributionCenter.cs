using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Domain.Entities
{
    public class DistributionCenter
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public DistributionCenter(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
