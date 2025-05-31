using OnlineOrder.Db.Models;

namespace OnlineOrderDb.Interface
{
    public interface IOrderServiceDb
    {
        Task AddOrderAsync(Order newOrder);
        Task<Order?> GetOrderAsync(Guid orderId);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
    }
}
