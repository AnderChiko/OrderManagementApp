using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Repositories;

namespace OrderManagement.IntegrationTests
{
    public class OrderServiceTests : IDisposable
    {
        private readonly AppDbContext _dbContext;
        private readonly OrderRepository _repo;
        private readonly IOrderService _service;
        private readonly SqliteConnection _connection;

        public OrderServiceTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();

            _repo = new OrderRepository(_dbContext);
            _service = new OrderService(_repo);
        }

        [Fact]
        public async Task AddAndGetOrder_ShouldReturnOrder()
        {
            var order = new Order { CustomerName = "Integration Test", OrderDate = DateTime.Today };
            await _service.AddAsync(order);

            var retrieved = await _service.GetByIdAsync(order.Id);
            Assert.NotNull(retrieved);
            Assert.Equal("Integration Test", retrieved!.CustomerName);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _connection.Close();
        }
    }
}
