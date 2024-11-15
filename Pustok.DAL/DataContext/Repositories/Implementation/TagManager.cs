using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class TagManager : Repository<Tag>, ITagRepository
{
    public TagManager(AppDBContext context) : base(context)
    {
        
    }
}
