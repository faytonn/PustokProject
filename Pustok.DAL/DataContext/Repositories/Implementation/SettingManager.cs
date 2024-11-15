using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class SettingManager : Repository<Setting>, ISettingRepository
{
    public SettingManager(AppDBContext context) : base(context)
    {
        
    }
}
