using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineOrder.Db.Models
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            var firstProduct = new Guid("68e896f8-c272-4b92-b52d-10d83e6452a2");
            var secondProduct = new Guid("86e210d8-3e55-4887-a957-55fa04bc7fc0");
            var thirdProduct = new Guid("734b060e-7385-4c35-bfad-2187c5d8fd6c");
            var fourthProduct = new Guid("5a6429bd-cc54-4252-a6ea-e370fcdada15");

            entityTypeBuilder.HasKey(e => e.ProductId);

            entityTypeBuilder.HasData(
                    new ProductBuilder()
                .WithId(firstProduct)
                .Build(),

                    new ProductBuilder()
                .WithId(secondProduct)
                .Build(),

                    new ProductBuilder()
                .WithId(thirdProduct)
                .Build(),

                    new ProductBuilder()
                .WithId(fourthProduct)
                .Build()
                );
        }
    }
}
