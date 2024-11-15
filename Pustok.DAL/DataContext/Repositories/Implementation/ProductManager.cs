using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class ProductManager : Repository<Product>, IProductRepository
{
    public ProductManager(AppDBContext context) : base(context)
    {
        
    }
}
