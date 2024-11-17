namespace Pustok.DAL.DataContext.Repositories.Abstraction;

public interface ISettingRepository : IRepository<Setting>
{
    Task<Dictionary<string, string>> GetLayoutSettingsAsync();
}
