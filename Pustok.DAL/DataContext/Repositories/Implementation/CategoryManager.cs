using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class CategoryManager : Repository<Category>, ICategoryRepository
{
    public CategoryManager(AppDBContext context) : base(context)
    {
        
    }
}
