using OnlineOrder.Db.Interface;
using OnlineOrder.Db.Models;
using OnlineOrderDb.Interface;
using Serilog;

namespace OnlineOrderDb.Service
{
    public class OrderServiceDb : IOrderServiceDb
    {
        private readonly IOrderDbRepository _dbRepository;


        public OrderServiceDb(IOrderDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public async Task AddOrderAsync(Order newOrder)
        {
            try
            {
                await _dbRepository.AddAsync(newOrder);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка создания заказа");
            }
        }

        public async Task<Order?> GetOrderAsync(Guid orderId)
        {
            try
            {
                return await _dbRepository.GetAsync(orderId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка добавления заказа {orderId}");
                return null;
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                await _dbRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка обновления заказа {order.Id}");
            }
        }

        public async Task DeleteOrderAsync(Order order)
        {
            try
            {
                await _dbRepository.DeleteAsync(order);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка удаления заказа {order.Id}");
            }
        }
    }
}
