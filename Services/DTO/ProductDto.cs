namespace OrdersOnlineWebApp.DTO
{
    public class ProductDto
    {
        private int qty;
        public Guid Id { get; set; }
        public int Qty
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
    }
}
