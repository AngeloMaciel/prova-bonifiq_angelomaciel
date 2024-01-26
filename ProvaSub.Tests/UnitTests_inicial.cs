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

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);//Testa funcionamento da classe de testes.
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

        [TestMethod]
        public async Task CanPurchase_NonExistentCustomer_ThrowsException()
        {
            // Arrange
            var customerId = 999;
            var purchaseValue = 50;
            var customerService = new CustomerService(_dbContext);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
                await customerService.CanPurchase(customerId, purchaseValue));
        }

        #endregion
        [TestMethod]
        public async Task CanPurchase_CustomerAlreadyPurchasedThisMonth_ReturnsFalse()
        {
            // Arrange
            var customerId = 1;
            var order = new Order { CustomerId = customerId, OrderDate = DateTime.UtcNow };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            var customerService = new CustomerService(_dbContext);

            // Act
            var result = await customerService.CanPurchase(customerId, 50);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CanPurchase_FirstPurchaseGreaterThan100_ReturnsFalse()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer { Id = customerId, Name = "Test Customer" };
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            var customerService = new CustomerService(_dbContext);

            // Act
            var result = await customerService.CanPurchase(customerId, 150);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CanPurchase_FirstPurchaseLessThanOrEqual100_ReturnsTrue()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer { Id = customerId, Name = "Test Customer" };
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            var customerService = new CustomerService(_dbContext);

            // Act
            var result = await customerService.CanPurchase(customerId, 100);

            // Assert
            Assert.IsTrue(result);
        }

        // Adicione outros métodos de teste conforme necessário...

    }
}