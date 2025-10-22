using CarCompany.Application.RepositoriesContracts;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCompany.Infrastructure.Repositories
{
    public class SalesRepository
    {
        private readonly List<Sale> _sales = new();
    }
}
