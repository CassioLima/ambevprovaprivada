using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(i => i.Id);
            builder.Property(s => s.Id)
                    .ValueGeneratedNever();

            builder.Property(i => i.SaleId)
                   .IsRequired()
                   .HasColumnType("uuid");

            builder.Property(i => i.ProductId)
                   .IsRequired()
                   .HasColumnType("uuid");

            builder.Property(i => i.ProductName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(i => i.Quantity).IsRequired();

            builder.Property(i => i.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(i => i.DiscountPercentage)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Ignore(i => i.Total);
            builder.Ignore(i => i.DiscountAmount);

            builder.HasOne(i => i.Sale)
                   .WithMany(s => s.Items)
                   .HasForeignKey(i => i.SaleId);
        }
    }
}
