namespace Pustok.BLL.ViewModels.AccountViewModels;

public class LoginViewModel : IViewModel
{
    public required string Email { set; get; }
    public required string Password { set; get; }
    public bool RememberMe { set; get; }
}
