using AutoMapper;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Paging;
using Pustok.DAL.DataContext.Repositories.Implementation;

namespace Pustok.BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductViewModel>().ReverseMap();    
        CreateMap<Product, CreateProductViewModel>().ReverseMap();
        CreateMap<Product, UpdateProductViewModel>().ReverseMap();
        CreateMap<Pagination<Product>, ProductListViewModel>().ReverseMap();

        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Category, CreateCategoryViewModel>().ReverseMap();
        CreateMap<Category, UpdateCategoryViewModel>().ReverseMap();
        CreateMap<Pagination<Category>, CategoryListViewModel>().ReverseMap();

        CreateMap<Subscribe, SubscribeViewModel>().ReverseMap();
        CreateMap<Subscribe, CreateSubscribeViewModel>().ReverseMap();
        CreateMap<Subscribe,  UpdateSubscribeViewModel>().ReverseMap();
        CreateMap<Pagination<Subscribe>, SubscribeListViewModel>().ReverseMap();

        CreateMap<Slider, SliderViewModel>().ReverseMap();
        CreateMap<Slider, CreateSliderViewModel>().ReverseMap();
        CreateMap<Slider, UpdateSliderViewModel>().ReverseMap();
        CreateMap<Pagination<Slider>, SliderListViewModel>().ReverseMap();  

        CreateMap<Tag, TagViewModel>().ReverseMap();
        CreateMap<Tag, CreateTagViewModel>().ReverseMap();
        CreateMap<Tag, UpdateTagViewModel>().ReverseMap();
        CreateMap<Pagination<Tag>, TagListViewModel>().ReverseMap();

        CreateMap<Attendance, AttendanceViewModel>().ReverseMap();
        CreateMap<Attendance, CreateAttendanceViewModel>().ReverseMap();
        CreateMap<Attendance, UpdateAttendanceViewModel>().ReverseMap();
        CreateMap<Pagination<Attendance>, AttendanceListViewModel>().ReverseMap();


    }
}
