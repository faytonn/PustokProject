using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pustok.DAL.DataContext.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Icon).IsRequired();
    }
}
