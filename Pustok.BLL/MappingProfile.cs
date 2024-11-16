using AutoMapper;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Implementation;

namespace Pustok.BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductViewModel>().ReverseMap();    
        CreateMap<Product, CreateProductViewModel>().ReverseMap();
        CreateMap<Product, UpdateProductViewModel>().ReverseMap();

        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Category, CreateCategoryViewModel>().ReverseMap();
        CreateMap<Category, UpdateCategoryViewModel>().ReverseMap();


    }
}
