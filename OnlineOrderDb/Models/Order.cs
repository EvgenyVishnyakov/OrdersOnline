namespace OnlineOrder.Db.Models
{
    public class Order
    {
        public Guid OrderId { get; init; } = Guid.NewGuid();
        public string created { get; init; }
        public Status Status { get; set; } = Status.New;
        public List<OrderProduct> OrderProducts { get; set; }
        public bool IsActiv { get; set; } = true;

        public Order() { }
    }
}
