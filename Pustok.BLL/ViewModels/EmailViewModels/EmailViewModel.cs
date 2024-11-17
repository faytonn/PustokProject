namespace Pustok.BLL.ViewModels.EmailViewModels;

public class EmailViewModel : IViewModel
{
    public required string ToEmail { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
    public bool IsHtml { get; set; } = true;
}
