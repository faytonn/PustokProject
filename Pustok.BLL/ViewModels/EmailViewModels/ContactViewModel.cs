namespace Pustok.BLL.ViewModels.EmailViewModels;

public class ContactViewModel : IViewModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Subject { get; set; }
    public required string Message { get; set; }
}
