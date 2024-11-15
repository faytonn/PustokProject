using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL.DataContext.Entities;

namespace Pustok.DAL.DataContext.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasOne(pi => pi.Product).WithMany(p => p.ProductImages).HasForeignKey(pi => pi.ProductId).OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.IsHover).HasDefaultValue(false);
        builder.Property(x => x.ImageUrl).IsRequired();
    }
}
