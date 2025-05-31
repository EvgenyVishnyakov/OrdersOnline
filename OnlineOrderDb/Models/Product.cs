using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineOrder.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        private int qty;
        [NotMapped]
        public int QTY { get; set; }

        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
