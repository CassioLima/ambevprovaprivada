using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                    .ValueGeneratedNever();

            builder.Property(s => s.SaleNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.Branch)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.PaymentMethod)
                   .HasMaxLength(50);

            builder.Property(s => s.Status)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(s => s.TotalAmount)
           .HasColumnType("numeric(18,2)")
           .IsRequired();

            builder.HasOne(s => s.Customer)
                   .WithMany(c => c.Sales)
                   .HasForeignKey(s => s.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Items)
                   .WithOne(i => i.Sale)
                   .HasForeignKey(i => i.SaleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
