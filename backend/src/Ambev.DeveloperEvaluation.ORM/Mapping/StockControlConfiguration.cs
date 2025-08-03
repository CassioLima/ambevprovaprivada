using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class StockControlConfiguration : IEntityTypeConfiguration<StockControl>
    {
        public void Configure(EntityTypeBuilder<StockControl> builder)
        {
            builder.ToTable("StockControl");

            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.ProductId)
                   .IsRequired()
                   .HasColumnType("uuid");

            builder.Property(sc => sc.MovementType)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(sc => sc.Quantity).IsRequired();

            builder.Property(sc => sc.MovementDate)
                   .IsRequired();

            builder.HasOne<Product>()
                   .WithMany(p => p.StockMovements)
                   .HasForeignKey(sc => sc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
