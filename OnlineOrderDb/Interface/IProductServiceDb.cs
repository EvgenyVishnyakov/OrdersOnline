using OnlineOrder.Db.Models;

namespace OnlineOrderDb.Interface
{
    public interface IProductServiceDb
    {
        Task<List<Product>> GetAllAsync(List<Guid> productIds);
    }
}
