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
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        void IEntityTypeConfiguration<DeliveryMethod>.Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(dm => dm.Cost)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
