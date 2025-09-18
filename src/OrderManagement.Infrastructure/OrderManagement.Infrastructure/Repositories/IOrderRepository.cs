using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Infrastructure.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
     
    }
}
