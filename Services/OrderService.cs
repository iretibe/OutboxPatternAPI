using OutboxPatternAPI.Data;
using OutboxPatternAPI.Models;

namespace OutboxPatternAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _appDbContext;

        public OrderService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Order> AddOrder(Order order)
        {
            if (order is not null)
            {
                await _appDbContext.Orders.AddAsync(order);
                await _appDbContext.SaveChangesAsync();
            }

            return order;
        }
    }
}
