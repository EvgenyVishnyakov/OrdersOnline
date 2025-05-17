using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineOrder.Db.Models
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => e.OrderId);

            entityTypeBuilder.Property(e => e.Created)
                .IsRequired();

            entityTypeBuilder.Property(e => e.Status)
                .IsRequired();
        }
    }
}
