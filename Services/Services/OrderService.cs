using OnlineOrder.Db.Models;
using OnlineOrderDb.Interface;
using OnlineOrderWebApp.DTO;
using OnlineOrderWebApp.Modes;
using OrdersOnlineWebApp.DTO;
using Serilog;

namespace BuisinessLogic.Services
{
    public class OrderService
    {
        private readonly IOrderServiceDb _orderServiceDb;
        private readonly IProductServiceDb _productServiceDb;
        public OrderService(IOrderServiceDb orderServiceDb, IProductServiceDb productServiceDb)
        {
            _orderServiceDb = orderServiceDb;
            _productServiceDb = productServiceDb;
        }
        public async Task<OrderResponseDto?> GetOrderAsync(Guid orderId)
        {
            try
            {
                var order = await _orderServiceDb.GetOrderAsync(orderId);
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
                var order = await _orderServiceDb.GetOrderAsync(orderId);
                if (order != null)
                {
                    if (Helper.IsBlockedForRemove(order))
                    {
                        await _orderServiceDb.DeleteOrderAsync(order);
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
                var order = await _orderServiceDb.GetOrderAsync(id);

                if (order == null)
                    throw new Exception("Заказ не найден");


                if (Helper.IsLockedForChange(order))
                {
                    Helper.GetStatus(status, order);
                    if (productsDto != null || productsDto.Count != 0)
                        await GetNewOrderProduct(productsDto, order);

                    await _orderServiceDb.UpdateOrderAsync(order);
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

                await _orderServiceDb.AddOrderAsync(newOrder);
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
            var products = await _productServiceDb.GetAllAsync(productIds);
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
