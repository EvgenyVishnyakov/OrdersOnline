using OnlineOrder.Db.Interface;
using OnlineOrder.Db.Models;
using Serilog;

namespace OnlineOrderWebApp.Service
{
    public class ProductService
    {
        private readonly IProductRepository _dbRepository;

        public ProductService(IProductRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public async Task<Product?> GetProductAsync(Guid productId)
        {
            try
            {
                return await _dbRepository.GetAsync(productId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка получения продукта!");
                return null;
            }
        }
    }
}
