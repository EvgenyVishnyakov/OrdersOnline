using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineOrder.Db.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        private int qty;
        [NotMapped]
        public int QTY
        {
            get
            {
                return qty;
            }
            set
            {
                if (value <= 0)
                    throw new Exception("Количество товара должно быть больше нуля");
                else
                    qty = value;
            }
        }

        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
