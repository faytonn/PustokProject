using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class BasketItemManager : Repository<BasketItem>, IBasketItemRepository
{
    public BasketItemManager(AppDBContext context) : base(context)
    {
        
    }
}
