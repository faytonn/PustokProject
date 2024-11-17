namespace Pustok.BLL.ViewModels;

public class SettingViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Key { get; set; }
    public string? Value { get; set; }
}

public class CreateSettingViewModel : IViewModel
{
    public required string Key { get; set; }
    public required string Value { get; set; }
}

public class UpdateSettingViewModel : IViewModel
{
    public int Id { get; set; }
    public required string Key { get; set; }
    public required string Value { get; set; }
}

public class SettingListViewModel : PageableViewModel, IViewModel
{
    public List<SettingViewModel> Items { get; set; } = [];
}