namespace Pustok.BLL.ViewModels.Base;

public class ResultViewModel<T> : IViewModel
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public T? Data { get; set; }
}
