using OnlineOrder.Db.Models;

namespace OnlineOrder.Db.Interface
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);

        Task<Product?> GetAsync(Guid productId);

        Task<List<Product>> GetAllAsync(List<Guid> productIds);
    }
}
