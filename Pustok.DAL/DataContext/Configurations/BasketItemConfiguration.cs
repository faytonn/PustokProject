using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL.DataContext.Entities;

namespace Pustok.DAL.DataContext.Configurations;

public class BasketItemConfigurations : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.HasOne(bi => bi.User).WithMany().HasForeignKey(bi => bi.UserId).OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Quantity).HasDefaultValue(0);
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.HasIndex(bi => bi.ProductId);
    }
}
