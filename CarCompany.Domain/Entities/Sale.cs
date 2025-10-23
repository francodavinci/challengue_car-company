using CarCompany.Domain.Enums;
using System.Diagnostics;
using System.Reflection;

namespace CarCompany.Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; }
    public Car Car { get; private set; }
    public Guid DistributionCenterID { get; private set; }

    public Sale(Car car, Guid distributionCenterID)
    {
        Id = Guid.NewGuid();
        Car = car;
        DistributionCenterID = distributionCenterID;
    }
}