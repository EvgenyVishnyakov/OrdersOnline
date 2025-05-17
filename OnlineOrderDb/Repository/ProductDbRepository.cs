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

        public async Task AddAsync(Product product)
        {
            _databaseContext.Products.Add(product);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Product?> GetAsync(Guid productId)
        {
            return await _databaseContext.Products
                .AsNoTracking()
                .Include(x => x.QTY)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<List<Product>> GetAllAsync(List<Guid> productIds)
        {
            return await _databaseContext.Products
                 .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();
        }
    }
}
