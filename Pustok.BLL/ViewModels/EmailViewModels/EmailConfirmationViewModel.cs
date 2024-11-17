namespace Pustok.BLL.ViewModels.EmailViewModels
{
    public class EmailConfirmationViewModel : IViewModel
    {
        public required string UserId { get; set; }
        public required string Token { get; set; }
        public required string ConfirmationLink { get; set; }
    }
}
