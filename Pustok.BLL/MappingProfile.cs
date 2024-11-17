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

        CreateMap<Subscribe, SubscribeViewModel>().ReverseMap();
        CreateMap<Subscribe, CreateSubscribeViewModel>().ReverseMap();
        CreateMap<Subscribe,  UpdateSubscribeViewModel>().ReverseMap();

        CreateMap<Slider, SliderViewModel>().ReverseMap();
        CreateMap<Slider, CreateSliderViewModel>().ReverseMap();
        CreateMap<Slider, UpdateSliderViewModel>().ReverseMap();

        CreateMap<Tag, TagViewModel>().ReverseMap();
        CreateMap<Tag, CreateTagViewModel>().ReverseMap();
        CreateMap<Tag, UpdateTagViewModel>().ReverseMap();


    }
}
