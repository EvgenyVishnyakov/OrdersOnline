using OnlineOrder.Db.Interface;
using OnlineOrder.Db.Models;
using OnlineOrderDb.Interface;

namespace OnlineOrderDb.Services
{
    public class ProductServiceDb : IProductServiceDb
    {
        private readonly IProductRepository _productDbRepository;

        public ProductServiceDb(IProductRepository productDbRepository)
        {
            _productDbRepository = productDbRepository;
        }

        public async Task<List<Product>> GetAllAsync(List<Guid> productIds)
        {
            return await _productDbRepository.GetAllAsync(productIds);
        }
    }
}
