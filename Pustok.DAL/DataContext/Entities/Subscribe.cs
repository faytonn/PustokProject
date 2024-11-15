using Pustok.DAL.DataContext.Entities.Base;

namespace Pustok.DAL.DataContext.Entities;

public class Subscribe : BaseEntity
{
    public required string Email { get; set; }
    public bool ConfirmedEmail { get; set; } = true;
}
