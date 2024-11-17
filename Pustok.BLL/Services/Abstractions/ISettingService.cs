using Microsoft.EntityFrameworkCore.Query;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using System.Linq.Expressions;

namespace Pustok.BLL.Services.Abstractions;

public interface ISettingService
{
    #region BaseMethods
    Task<SettingViewModel> CreateSettingAsync(CreateSettingViewModel model);
    Task<SettingViewModel> UpdateSettingAsync(int id, UpdateSettingViewModel model);
    Task<SettingViewModel> DeleteSettingAsync(int SettingId);
    Task<SettingViewModel> GetSettingByIdAsync(int SettingId);
    Task<SettingListViewModel> GetPaginatedSettingsAsync(int pageNumber, int pageSize);
    Task<SettingViewModel?> GetAsync(Expression<Func<Setting, bool>> predicate, Func<IQueryable<Setting>, IIncludableQueryable<Setting, object>>? include = null);

    Task<SettingListViewModel> GetAllAsync(
        Expression<Func<Setting, bool>> predicate,
        Func<IQueryable<Setting>, IIncludableQueryable<Setting, object>>? include = null,
        Func<IQueryable<Setting>, IOrderedQueryable<Setting>>? orderBy = null
    );
    #endregion
    Task<bool> DoesKeyAlreadyExistAsync(string key);
    Task<Dictionary<string, string>> GetLayoutSettingsAsync();
}
