
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Repositories;

namespace OrderManagement.Application.Services
    {
        public class OrderService : GenericService<Order> , IOrderService
        {
            private readonly IOrderRepository _orderRepository;

            public OrderService(IOrderRepository orderRepository)
                : base(orderRepository) // Generic CRUD
            {
                _orderRepository = orderRepository;
            }

            // Example: custom order logic
            public decimal CalculateTotal(Order order)
            {
                return order.Lines.Sum(l => l.Quantity * l.UnitPrice);
            }
        }
    }

