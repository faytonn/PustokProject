using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class SettingManager : Repository<Setting>, ISettingRepository
{
    private readonly AppDBContext _dbContext;
    public SettingManager(AppDBContext context) : base(context)
    {
        _dbContext = context;   
    }
    public async Task<Dictionary<string, string>> GetLayoutSettingsAsync()
    {
        Dictionary<string, string> settings = await _dbContext.Setting.ToDictionaryAsync(x => x.Key, x => x.Value);

        return settings;
    }
}
