using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Repositories;
using Xunit;

namespace OrderManagement.IntegrationTests
{
    public class OrderRepositoryTests : IDisposable
    {
        private readonly AppDbContext _dbContext;
        private readonly IOrderRepository _repository;
        private readonly SqliteConnection _connection;

        public OrderRepositoryTests()
        {
            // In-memory SQLite connection
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();

            _repository = new OrderRepository(_dbContext);
        }

        [Fact]
        public async Task AddOrder_ShouldPersistOrder()
        {
            // Arrange
            var order = new Order
            {
                CustomerName = "John Doe",
                OrderDate = DateTime.Today
            };

            // Act
            await _repository.AddAsync(order);
            var retrieved = await _repository.GetByIdAsync(order.Id);

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal("John Doe", retrieved!.CustomerName);
        }

        [Fact]
        public async Task UpdateOrder_ShouldPersistChanges()
        {
            var order = new Order { CustomerName = "Alice", OrderDate = DateTime.Today };
            await _repository.AddAsync(order);

            order.CustomerName = "Alice Updated";
            await _repository.UpdateAsync(order);

            var retrieved = await _repository.GetByIdAsync(order.Id);
            Assert.Equal("Alice Updated", retrieved!.CustomerName);
        }

        [Fact]
        public async Task DeleteOrder_ShouldRemoveOrder()
        {
            var order = new Order { CustomerName = "Bob", OrderDate = DateTime.Today };
            await _repository.AddAsync(order);

            await _repository.DeleteAsync(order);

            var retrieved = await _repository.GetByIdAsync(order.Id);
            Assert.Null(retrieved);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _connection.Close();
        }
    }
}
