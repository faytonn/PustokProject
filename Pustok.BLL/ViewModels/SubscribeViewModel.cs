namespace Pustok.BLL.ViewModels;

public class SubscribeViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public bool ConfirmedEmail { get; set; } = false;
}

public class CreateSubscribeViewModel : IViewModel
{
    public string? Email { set; get; }
    public bool ConfirmedEmail { get; set; } = false;
}

public class UpdateSubscribeViewModel : IViewModel
{
    public int Id { set; get; }
    public string? Email { set; get; }
    public bool ConfirmedEmail { get; set;} = false;
}