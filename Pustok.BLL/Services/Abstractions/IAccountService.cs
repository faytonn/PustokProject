using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pustok.BLL.ViewModels.AccountViewModels;

namespace Pustok.BLL.Services.Abstractions;

public interface IAccountService 
{
    Task<bool> RegisterAsync(RegisterViewModel vm, ModelStateDictionary modelState);
    Task<bool> LoginAsync(LoginViewModel vm, ModelStateDictionary modelState);
    Task<bool> SignOutAsync();
}
