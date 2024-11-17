using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction.Generic;
using System.Linq.Expressions;

namespace Pustok.BLL.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryViewModel> CreateCategoryAsync(CreateCategoryViewModel model)
    {
        var category = _mapper.Map<Category>(model);
        var createdCategory = await _categoryRepository.CreateAsync(category);
        return _mapper.Map<CategoryViewModel>(createdCategory);
    }

    public async Task<CategoryViewModel> UpdateCategoryAsync(int id, UpdateCategoryViewModel model)
    {
        var existingCategory = await _categoryRepository.GetAsync(id);
        if (existingCategory == null) throw new Exception("Category not found.");

        _mapper.Map(model, existingCategory);
        var updatedCategory = await _categoryRepository.UpdateAsync(existingCategory);
        return _mapper.Map<CategoryViewModel>(updatedCategory);
    }

    public async Task<CategoryViewModel> DeleteCategoryAsync(int categoryId)
    {
        var existingCategory = await _categoryRepository.GetAsync(categoryId);
        if (existingCategory == null) throw new Exception("Category not found.");

        var deletedCategory = await _categoryRepository.DeleteAsync(existingCategory);
        return _mapper.Map<CategoryViewModel>(deletedCategory);
    }

    public async Task<CategoryListViewModel> GetAllAsync(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null, Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null)
    {
        predicate ??= c => true;

        var categories = await _categoryRepository.GetAllAsync(predicate, include, orderBy);
        var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categories);

        return new CategoryListViewModel
        {
            Items = categoryViewModels,
            Count = categoryViewModels.Count,
            Index = 0,
            Size = categoryViewModels.Count,
            Pages = 1,
            HasPrevious = false,
            HasNext = false
        };
    }

    public async Task<CategoryViewModel?> GetAsync(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
    {
        var category = await _categoryRepository.GetAsync(predicate, include);
        return category == null ? null : _mapper.Map<CategoryViewModel>(category);
    }

    public async Task<CategoryViewModel> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetAsync(categoryId);
        if (category == null) throw new Exception("Category not found.");

        return _mapper.Map<CategoryViewModel>(category);
    }

    public async Task<CategoryListViewModel> GetPaginatedCategoriesAsync(int pageNumber, int pageSize)
    {
        var categories = await _categoryRepository.GetPagesAsync(index: pageNumber, size: pageSize);
        var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categories.Items);

        return new CategoryListViewModel
        {
            Index = pageNumber,
            Size = pageSize,
            Items = categoryViewModels,
            Count = categories.Count,
            Pages = categories.Pages
        };
    }

    public async Task<List<CategoryViewModel>> GetParentCategoriesAsync()
    {
        var list = await _categoryRepository.GetAllAsync(x => x.ParentId == null);

        var selectListItems = new List<SelectListItem>();

        foreach (var item in list)
        {
            selectListItems.Add(new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });
        }

        return _mapper.Map<List<CategoryViewModel>>(list);
    }

    public async Task<List<CategoryViewModel>> GetChildCategoriesAsync(int parentId)
    {
        var list = await _categoryRepository.GetAllAsync(x => x.ParentId == parentId);

        return _mapper.Map<List<CategoryViewModel>>(list);
    }

    public async Task<CategoryViewModel> RemoveAndOrphanChildCategories(int id)
    {
        var category = await _categoryRepository.GetAsync(id);

        var childCategories = await GetChildCategoriesAsync(id);

        if(childCategories != null)
        {
            var childCategoryVMs = _mapper.Map<List<Category>>(childCategories);

            foreach(var childCategory in childCategoryVMs)
            {
                childCategory.ParentId = null;

                await _categoryRepository.UpdateAsync(childCategory);
            }
        }

        var deletedCategory = await _categoryRepository.DeleteAsync(category);

        return _mapper.Map<CategoryViewModel>(deletedCategory);
    }

}
