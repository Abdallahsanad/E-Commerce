using Microsoft.EntityFrameworkCore;
using Store.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18, 2)");

            builder.Property(o => o.Status)
                .HasConversion(ostatus => ostatus.ToString(), ostatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), ostatus));

            builder.OwnsOne(o => o.ShippingAddress, sh => sh.WithOwner());

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId);
        }
    }
}
