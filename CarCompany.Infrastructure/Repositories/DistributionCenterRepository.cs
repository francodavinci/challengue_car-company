using System;
using System.Collections.Generic;
using CarCompany.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarCompany.Application.RepositoriesContracts;

namespace CarCompany.Infrastructure.Repositories
{
    public class DistributionCenterRepository : IDistributionCenterRepositoryContract
    {
        private readonly List<DistributionCenter> _distributionCenters = new()
        {
            new DistributionCenter ( Guid.Parse("71EDF1A7-032E-4435-8DA5-C5A9E99B3026") ,"TOYOTA - BELLA VISTA"),
            new DistributionCenter ( Guid.Parse("3A991B26-02AC-4926-ADE9-C0BE743A7556"), "TOYOTA - LANUS"),
            new DistributionCenter ( Guid.Parse("8234AC68-0294-4C9F-BE95-A10488E4B401"), "TOYOTA - VILLA CRESPO"),
            new DistributionCenter ( Guid.Parse("F61AECA3-7F02-48A7-864E-AD5B14155C94"), "TOYOTA - OLAVARRIA"),
        };

        public IEnumerable<DistributionCenter> GetAll() => _distributionCenters;

        public DistributionCenter GetById(Guid id) => _distributionCenters.FirstOrDefault(x => x.Id == id);
    }
}
