namespace Pustok.BLL.ViewModels.AccountViewModels;

public class ResetPasswordViewModel : IViewModel
{
    public required string Email { get; set; }
    public required string Token { get; set; }
    public required string NewPassword { get; set; }
    public required string ConfirmPassword { get; set; }
}
