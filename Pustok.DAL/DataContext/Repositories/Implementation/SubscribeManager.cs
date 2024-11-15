using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class SubscribeManager : Repository<Subscribe>, ISubscribeRepository
{
    public SubscribeManager(AppDBContext context) : base(context)
    {
        
    }
}
