namespace Pustok.BLL.ViewModels.AccountViewModels;

public class RegisterViewModel : IViewModel
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}
