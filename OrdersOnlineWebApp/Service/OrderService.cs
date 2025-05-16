using OnlineOrder.Db.Interface;
using OnlineOrder.Db.Models;
using OnlineOrderWebApp.DTO;
using OnlineOrderWebApp.Modes;
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
                    var blockedStatuses = new[] { Status.Delivared, Status.Delivering, Status.Done };

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

        public async Task<OrderResponseDto?> UpdateAsync(Guid id, Status status, Dictionary<Guid, int> productQuantities)
        {
            try
            {
                var order = await _dbRepository.GetAsync(id);

                if (order == null)
                    throw new Exception("Заказ не найден");


                if (Helper.IsLockedForChange(order))
                {
                    if (productQuantities == null || productQuantities.Count == 0)
                    {
                        Helper.GetStatus(status, order);
                    }
                    else
                    {
                        Helper.GetStatus(status, order);

                        var productIds = productQuantities.Keys.ToList();
                        await GetNewOrderProduct(productQuantities, order, productIds);
                    }

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

        public async Task<OrderResponseDto?> CreateAsync(Dictionary<Guid, int> data)
        {
            try
            {
                var created = Helper.GetDate();
                var productIds = data.Keys.ToList();
                var products = await _productDbRepository.GetAllAsync(productIds);

                var orderProducts = Helper.GetNewOrderProduct(data, products);
                var newOrder = Helper.GetNewOrder(created, orderProducts);

                await _dbRepository.AddAsync(newOrder);
                Log.Information($"Создан новый заказ под номером {newOrder.OrderId}");

                return Helper.GetOrderResponse(newOrder);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка создания заказа!");
                return null;
            }
        }

        private async Task GetNewOrderProduct(Dictionary<Guid, int> productQuantities, Order? order, List<Guid> productIds)
        {
            var products = await _productDbRepository.GetAllAsync(productIds);
            order.OrderProducts = products.Select(p => new OrderProduct
            {
                ProductId = p.ProductId,
                OrderId = order.OrderId,
                ProductCount = productQuantities[p.ProductId]
            }).ToList();
        }
    }
}
