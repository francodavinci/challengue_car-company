using Xunit;
using CarCompany.Infrastructure.Repositories;
using CarCompany.Domain.Entities;

namespace CarCompany.Tests.Repositories
{
    public class DistributionCenterRepositoryTests
    {
        private readonly DistributionCenterRepository _repository;

        public DistributionCenterRepositoryTests()
        {
            _repository = new DistributionCenterRepository();
        }

        [Fact]
        public void GetAll_ShouldReturnAllDistributionCenters()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All(result, dc => Assert.NotNull(dc.Name));
        }

        [Fact]
        public void GetById_WithValidId_ShouldReturnCorrectCenter()
        {
            // Arrange
            var allCenters = _repository.GetAll();
            var firstCenter = allCenters.First();

            // Act
            var result = _repository.GetById(firstCenter.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(firstCenter.Id, result.Id);
            Assert.Equal(firstCenter.Name, result.Name);
        }

        [Fact]
        public void GetById_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = _repository.GetById(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAll_ShouldReturnConsistentData()
        {
            // Act
            var result1 = _repository.GetAll();
            var result2 = _repository.GetAll();

            // Assert
            Assert.Equal(result1.Count(), result2.Count());
            Assert.All(result1, dc1 => 
            {
                var dc2 = result2.FirstOrDefault(dc => dc.Id == dc1.Id);
                Assert.NotNull(dc2);
                Assert.Equal(dc1.Name, dc2.Name);
            });
        }

        [Fact]
        public void GetById_WithEmptyGuid_ShouldReturnNull()
        {
            // Arrange
            var emptyGuid = Guid.Empty;

            // Act
            var result = _repository.GetById(emptyGuid);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAll_ShouldReturnUniqueIds()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            var ids = result.Select(dc => dc.Id).ToList();
            Assert.Equal(ids.Count, ids.Distinct().Count());
        }
    }
}
