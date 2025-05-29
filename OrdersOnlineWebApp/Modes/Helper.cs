using OnlineOrder.Db.Models;
using OnlineOrderWebApp.DTO;

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

        public static Order GetNewOrder(string created, List<OrderProduct> orderProducts)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                CreatedOrder = created,
                Status = Status.New,
                OrderProducts = orderProducts
            };
        }

        public static List<OrderProduct> GetNewOrderProduct(Dictionary<Guid, int> data, List<Product> products)
        {
            return products.Select(p => new OrderProduct
            {
                ProductId = p.Id,

                ProductCount = data[p.Id]
            }).ToList();
        }

        public static string GetDate()
        {
            var nowDay = DateTime.Now;
            return nowDay.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static OrderResponseDto GetOrderResponse(Order newOrder)
        {
            return new OrderResponseDto
            {
                Id = newOrder.Id,
                Status = newOrder.Status.ToString(),
                Created = newOrder.CreatedOrder,
                Lines = newOrder.OrderProducts.Select(op => new OrderLineDto
                {
                    Id = op.ProductId,
                    Qty = op.ProductCount
                }).ToList()
            };
        }
    }
}
