using Pustok.DAL.DataContext.Entities.Base;

namespace Pustok.DAL.DataContext.Entities;

public class Attendance : BaseEntity
{
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}
