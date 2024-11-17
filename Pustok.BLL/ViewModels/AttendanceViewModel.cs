namespace Pustok.BLL.ViewModels;

public class AttendanceViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Icon { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}

public class CreateAttendanceViewModel : IViewModel
{
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}

public class UpdateAttendanceViewModel : IViewModel
{
    public int Id { get; set; }
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}

public class AttendanceListViewModel : PageableViewModel, IViewModel
{
    public List<AttendanceViewModel> Items { get; set; } = new List<AttendanceViewModel>();
}
