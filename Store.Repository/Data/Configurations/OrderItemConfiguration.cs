using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(oi => oi.Price)
                .HasColumnType("decimal(18, 2)");

            builder.OwnsOne(oi => oi.Product, p => p.WithOwner());
        }
    }
}
