using OnlineOrder.Db.Models;
using OnlineOrderWebApp.DTO;
using OrdersOnlineWebApp.DTO;

namespace OnlineOrderWebApp.Modes
{
    public static class Helper
    {
        public static bool IsBlockedForRemove(Order order)
        {
            var blockedStatuses = new[] { Status.Delivared, Status.Delivering, Status.Done };
            return !blockedStatuses.Contains(order.Status);
        }

        public static void GetStatus(Status status, Order? order)
        {
            if ((int)status > (int)order.Status)
                order.Status = status;
        }

        public static bool IsLockedForChange(Order order)
        {
            var lockedStatuses = new[] { Status.Paid, Status.Delivering, Status.Delivared, Status.Done };
            return !lockedStatuses.Contains(order.Status);
        }

        public static Order GetNewOrder(string created, List<ProductDto> productDtos)
        {
            var ordersProduct = GetNewOrderProduct(productDtos);

            return new Order
            {
                DataCreatedOrder = created,
                Status = Status.New,
                OrderProducts = ordersProduct
            };
        }

        public static List<OrderProduct> GetNewOrderProduct(List<ProductDto> productDtos)
        {
            var orderProducts = new List<OrderProduct>();

            foreach (var productDto in productDtos)
            {
                orderProducts.Add(new OrderProduct
                {
                    ProductId = productDto.Id,
                    ProductCount = productDto.Qty
                });
            }

            return orderProducts;
        }

        public static OrderResponseDto GetOrderResponse(Order newOrder)
        {
            return new OrderResponseDto
            {
                Id = newOrder.Id,
                Status = newOrder.Status.ToString(),
                DataCreatedOrder = newOrder.DataCreatedOrder,
                Lines = newOrder.OrderProducts.Select(op => new OrderLineDto
                {
                    Id = op.ProductId,
                    Qty = op.ProductCount
                }).ToList()
            };
        }
    }
}
