namespace OnlineOrder.Db.Models
{
    public class ProductBuilder
    {
        private readonly Product _product = new Product();

        public ProductBuilder WithId(Guid id)
        {
            _product.ProductId = id;
            return this;
        }

        public Product Build()
        {
            return _product;
        }
    }
}
