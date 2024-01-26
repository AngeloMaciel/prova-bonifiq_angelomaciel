using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaSub.Tests
{
    [TestClass]
    public class UnitTests_inicial
    {

        private TestDbContext _dbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            var projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../prova-bonifiq"));

            var configuration = new ConfigurationBuilder()
            .SetBasePath(projectDir)
            .AddJsonFile("appsettings.json")
            .Build();

            var serviceProvider = new ServiceCollection()
            .AddDbContext<TestDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ctx")))
            .BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<TestDbContext>();
        }

        #region Cenarios CanPurchase

        [TestMethod]
        public async Task CanPurchase_ValidPurchase_ReturnsTrue()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 50;
            var customerService = new CustomerService(_dbContext);

            // Act
            var result = await customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
