using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using System.Linq.Expressions;

namespace Pustok.BLL.Services.Implementations;

public class SettingService : ISettingService
{
    private readonly IMapper _mapper;
    private readonly ISettingRepository _settingRepository;

    public SettingService(ISettingRepository settingRepository, IMapper mapper)
    {
        _mapper = mapper;
        _settingRepository = settingRepository;
    }
    public async Task<SettingViewModel> CreateSettingAsync(CreateSettingViewModel model)
    {
        var setting = _mapper.Map<Setting>(model);
        var createdSetting = await _settingRepository.CreateAsync(setting);
        return _mapper.Map<SettingViewModel>(createdSetting);
    }

    public async Task<SettingViewModel> UpdateSettingAsync(int id, UpdateSettingViewModel model)
    {
        var existingSetting = await _settingRepository.GetAsync(id);
        if (existingSetting == null) throw new Exception("Setting not found.");

        _mapper.Map(model, existingSetting);
        var updatedSetting = await _settingRepository.UpdateAsync(existingSetting);
        return _mapper.Map<SettingViewModel>(updatedSetting);
    }

    public async Task<SettingViewModel> DeleteSettingAsync(int settingId)
    {
        var existingSetting = await _settingRepository.GetAsync(settingId);
        if (existingSetting == null) throw new Exception("Setting not found.");

        var deletedSetting = await _settingRepository.DeleteAsync(existingSetting);
        return _mapper.Map<SettingViewModel>(deletedSetting);
    }

    public async Task<SettingListViewModel> GetAllAsync(Expression<Func<Setting, bool>> predicate, Func<IQueryable<Setting>, IIncludableQueryable<Setting, object>>? include = null, Func<IQueryable<Setting>, IOrderedQueryable<Setting>>? orderBy = null)
    {
        predicate ??= c => true;

        var categories = await _settingRepository.GetAllAsync(predicate, include, orderBy);
        var settingViewModels = _mapper.Map<List<SettingViewModel>>(categories);

        return new SettingListViewModel
        {
            Items = settingViewModels,
            Count = settingViewModels.Count,
            Index = 0,
            Size = settingViewModels.Count,
            Pages = 1,
            HasPrevious = false,
            HasNext = false
        };
    }

    public async Task<SettingViewModel> GetSettingByIdAsync(int settingId)
    {
        var setting = await _settingRepository.GetAsync(settingId);
        if (setting == null) throw new Exception("setting not found.");

        return _mapper.Map<SettingViewModel>(setting);
    }
    public async Task<SettingViewModel?> GetAsync(Expression<Func<Setting, bool>> predicate, Func<IQueryable<Setting>, IIncludableQueryable<Setting, object>>? include = null)
    {
        var setting = await _settingRepository.GetAsync(predicate, include);
        return setting == null ? null : _mapper.Map<SettingViewModel>(setting);
    }
    public async Task<SettingListViewModel> GetPaginatedSettingsAsync(int pageNumber, int pageSize)
    {
        var settings = await _settingRepository.GetPagesAsync(index: pageNumber, size: pageSize);
        var settingViewModels = _mapper.Map<List<SettingViewModel>>(settings.Items);

        return new SettingListViewModel
        {
            Index = pageNumber,
            Size = pageSize,
            Items = settingViewModels,
            Count = settings.Count,
            Pages = settings.Pages
        };
    }
    public async Task<bool> DoesKeyAlreadyExistAsync(string key)
    {
        var setting = await _settingRepository.GetAsync(x => x.Key == key);

        if (setting == null) return false;

        return true;
    }


    public async Task<Dictionary<string, string>> GetLayoutSettingsAsync()
    {
        Dictionary<string, string> settings = await _settingRepository.GetLayoutSettingsAsync();

        return settings;
    }

}
