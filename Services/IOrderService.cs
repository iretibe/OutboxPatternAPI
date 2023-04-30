using OutboxPatternAPI.Models;

namespace OutboxPatternAPI.Services
{
    public interface IOrderService
    {
        Task<Order> AddOrder(Order order);
    }
}
