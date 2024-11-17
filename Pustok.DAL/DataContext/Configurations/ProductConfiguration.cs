using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pustok.DAL.DataContext.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(1024);
        builder.Property(x => x.OriginalPrice).HasDefaultValue(0).HasColumnType("decimal(10,2)");
        builder.Property(x => x.DiscountPrice).HasDefaultValue(0).HasColumnType("decimal(10,2)");
        builder.Property(x => x.InStock).HasDefaultValue(false);
    }
}
