using Pustok.DAL.DataContext.Entities.Base;

namespace Pustok.DAL.DataContext.Entities;

public class Setting : BaseEntity
{
    public required string Key { get; set; }
    public required string Value { get; set; }
}
