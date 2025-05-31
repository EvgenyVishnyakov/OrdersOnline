using OnlineOrder.Db.Interface;
using OnlineOrder.Db.Models;
using OnlineOrderWebApp.DTO;
using OnlineOrderWebApp.Modes;
using OrdersOnlineWebApp.DTO;
using Serilog;

namespace OnlineOrderWebApp.Service
{
    public class OrderService
    {
        private readonly IOrderDbRepository _dbRepository;
        private readonly IProductRepository _productDbRepository;

        public OrderService(IOrderDbRepository dbRepository, IProductRepository productDbRepository)
        {
            _dbRepository = dbRepository;
            _productDbRepository = productDbRepository;
        }

        public async Task<OrderResponseDto?> GetOrderAsync(Guid orderId)
        {
            try
            {
                var order = await _dbRepository.GetAsync(orderId);
                return Helper.GetOrderResponse(order);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка получения заказа");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid orderId)
        {
            try
            {
                var order = await _dbRepository.GetAsync(orderId);
                if (order != null)
                {
                    if (Helper.IsBlockedForRemove(order))
                    {
                        await _dbRepository.DeleteAsync(order);
                        Log.Information($"Заказ {orderId} удален");
                        return true;
                    }
                    else
                    {
                        Log.Warning($"Заказ {orderId} уже не может быть удален (статус: {order.Status})!");
                    }
                }

                Log.Warning($"Заказ {orderId} не найден для удаления");
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка удаления заказа");
                return false;
            }
        }

        public async Task<OrderResponseDto?> UpdateAsync(Guid id, Status status, List<ProductDto> productsDto)
        {
            try
            {
                var order = await _dbRepository.GetAsync(id);

                if (order == null)
                    throw new Exception("Заказ не найден");


                if (Helper.IsLockedForChange(order))
                {
                    Helper.GetStatus(status, order);
                    if (productsDto != null || productsDto.Count != 0)
                        await GetNewOrderProduct(productsDto, order);

                    await _dbRepository.UpdateAsync(order);
                }

                return Helper.GetOrderResponse(order);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка обновления заказа!");
                return null;
            }
        }

        public async Task<OrderResponseDto?> CreateAsync(List<ProductDto> productsDto)
        {
            try
            {
                var datetimeCreatedOrder = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (!await IsProduct(productsDto))
                    throw new Exception("Не все продукты есть в наличии");

                var newOrder = Helper.GetNewOrder(datetimeCreatedOrder, productsDto);

                await _dbRepository.AddAsync(newOrder);
                Log.Information($"Создан новый заказ под номером {newOrder.Id}");

                return Helper.GetOrderResponse(newOrder);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка создания заказа!");
                return null;
            }
        }

        private async Task<bool> IsProduct(List<ProductDto> productsDto)
        {
            var productIds = productsDto.Select(p => p.Id).ToList();
            var products = await _productDbRepository.GetAllAsync(productIds);
            return productIds.Count == products.Count;
        }

        private async Task GetNewOrderProduct(List<ProductDto> productsDto, Order? order)
        {
            try
            {
                if (!await IsProduct(productsDto))
                    throw new Exception("Не все продукты есть в наличии");

                var productQuantities = productsDto.ToDictionary(p => p.Id, p => p.Qty);

                order.OrderProducts = productsDto.Select(p => new OrderProduct
                {
                    ProductId = p.Id,
                    OrderId = order.Id,
                    ProductCount = productQuantities.TryGetValue(p.Id, out var quantity) ? quantity : 0
                }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка обновления заказа {ex.Message}");
                throw;
            }
        }
    }
}
