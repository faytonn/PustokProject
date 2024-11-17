using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pustok.DAL.DataContext.Configurations;

public class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.Property(x => x.ImageUrl).IsRequired();
        builder.Property(x => x.Subtitle).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(255);

    }
}