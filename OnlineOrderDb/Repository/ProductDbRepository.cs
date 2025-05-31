using Microsoft.EntityFrameworkCore;
using OnlineOrder.Db.Interface;
using OnlineOrder.Db.Models;

namespace OnlineOrder.Db.Repository
{
    public class ProductDbRepository : IProductRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ProductDbRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Product>> GetAllAsync(List<Guid> productIds)
        {
            return await _databaseContext.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();
        }
    }
}
