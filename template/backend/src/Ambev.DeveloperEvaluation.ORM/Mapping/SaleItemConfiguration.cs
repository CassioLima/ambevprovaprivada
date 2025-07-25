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

            // Não há Id explícito, então ProductId será usado como chave primária
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductId)
                   .HasColumnType("uuid");

            builder.Property(i => i.ProductDescription)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(i => i.Quantity)
                   .IsRequired();

            builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");

            builder.Property(i => i.DiscountPercentage)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            // Campos calculados não são persistidos
            builder.Ignore(i => i.Total);
            builder.Ignore(i => i.DiscountAmount);
        }
    }
}
