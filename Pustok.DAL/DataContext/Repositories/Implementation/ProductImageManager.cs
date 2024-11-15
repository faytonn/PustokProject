using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class ProductImageManager : Repository<ProductImage>, IProductImageRepository
{
    public ProductImageManager(AppDBContext context) : base(context)
    {
        
    }
}
