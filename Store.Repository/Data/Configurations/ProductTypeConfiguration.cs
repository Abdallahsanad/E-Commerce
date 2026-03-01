using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        void IEntityTypeConfiguration<ProductType>.Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(P => P.Id);
            builder.Property(P => P.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(P => P.Name)
                .HasMaxLength(150);
        }
    }
}
