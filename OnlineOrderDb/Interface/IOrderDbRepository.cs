using OnlineOrder.Db.Models;

namespace OnlineOrder.Db.Interface
{
    public interface IOrderDbRepository
    {
        Task AddAsync(Order order);

        Task<Order?> GetAsync(Guid orderId);

        Task<List<Order>> GetAllAsync();

        Task UpdateAsync(Order order);

        Task UpdateStatusAsync(Guid orderId, Status status);

        Task DeleteAsync(Order deleteOrder);
    }
}
