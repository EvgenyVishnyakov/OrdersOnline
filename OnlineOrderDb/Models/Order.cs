namespace OnlineOrder.Db.Models
{
    public class Order
    {
        public Guid Id { get; init; }
        public string CreatedOrder { get; init; }
        public Status Status { get; set; } = Status.New;
        public List<OrderProduct> OrderProducts { get; set; }
        public bool IsActiv { get; set; } = true;
    }
}
