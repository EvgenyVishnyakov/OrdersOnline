using Microsoft.EntityFrameworkCore;
using OnlineOrder.Db.Interface;
using OnlineOrder.Db.Models;

namespace OnlineOrder.Db.Repository
{
    public class OrderDbRepository : IOrderDbRepository
    {
        private readonly DatabaseContext _databaseContext;
        public OrderDbRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AddAsync(Order order)
        {
            await _databaseContext.Orders.AddAsync(order);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Order?> GetAsync(Guid orderId)
        {
            return await _databaseContext.Orders
                .AsNoTracking()
                .Where(o => o.IsActiv == true)
                .Include(op => op.OrderProducts)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _databaseContext.Orders
                .Where(o => o.IsActiv == true)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _databaseContext.Orders.Update(order);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(Guid orderId, Status status)
        {
            var order = await GetAsync(orderId);
            order!.Status = status;
            _databaseContext.Orders?.Update(order);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order deleteOrder)
        {
            var order = await GetAsync(deleteOrder.OrderId);
            order.IsActiv = false;
            _databaseContext.Orders.Update(order);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
