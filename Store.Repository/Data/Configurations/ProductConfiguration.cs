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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(P => P.Id);
            builder.Property(P => P.Id)
                .UseIdentityColumn(1,1);

            builder.Property(P => P.Name)
                .HasMaxLength(150);

            builder.Property(P => P.Price)
                .HasColumnType("decimal(10, 2)");

            builder.HasOne(P=>P.Brand)
                .WithMany()
                .HasForeignKey(P=>P.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(P=>P.Type)
                .WithMany()
                .HasForeignKey(P=>P.TypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
