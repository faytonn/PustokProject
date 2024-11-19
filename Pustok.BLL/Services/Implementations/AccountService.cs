using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels.AccountViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Enums;

namespace Pustok.BLL.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public AccountService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
    }

    public async Task<bool> LoginAsync(LoginViewModel vm, ModelStateDictionary modelState)
    {
        if (_contextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
        {
            modelState.AddModelError("", "User already signed");
            return false;
        }
        if (!modelState.IsValid)
            return false;

        var user = await _userManager.FindByEmailAsync(vm.Email);

        if (user is null)
            user = await _userManager.FindByNameAsync(vm.Email);

        if (user is null)
        {
            modelState.AddModelError("", "Username or password is incorrect");
            return false;
        }

        if (user.LockoutEnabled)
            return false;

        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
        if (!result.Succeeded)
        {
            modelState.AddModelError("", "Username or password is incorrect");
            return false;
        }

        return true;
    }

    public async Task<bool> RegisterAsync(RegisterViewModel vm, ModelStateDictionary modelState)
    {
        if (_contextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
        {
            modelState.AddModelError("", "User already signed");
            return false;
        }
        if (!modelState.IsValid)
            return false;

        if (vm.ConfirmPassword != vm.Password)
        {
            modelState.AddModelError("", "Passwords don't match");
            return false;
        }

        var newUser = _mapper.Map<AppUser>(vm);
        newUser.LockoutEnabled = false;

        var result = await _userManager.CreateAsync(newUser, vm.Password);
        await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());

        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                modelState.AddModelError("", item.Description);
            }
            return false;
        }

        return true;
    }

    public async Task<bool> SignOutAsync()
    {
        await _signInManager.SignOutAsync();

        return true;
    }
}
