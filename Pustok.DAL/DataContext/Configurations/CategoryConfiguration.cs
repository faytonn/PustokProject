using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pustok.DAL.DataContext.Entities;

namespace Pustok.DAL.DataContext.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.ParentId).IsRequired(false);

        builder.HasOne(c => c.ParentCategory).WithMany(c => c.ChildCategories).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => c.ParentId);

    }
}
