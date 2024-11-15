using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class SliderManager : Repository<Slider>, ISliderRepository
{
    public SliderManager(AppDBContext context) : base(context)
    {
        
    }
}
