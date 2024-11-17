﻿using Microsoft.EntityFrameworkCore.Query;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using System.Linq.Expressions;

namespace Pustok.BLL.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> CreateCategoryAsync(CreateCategoryViewModel model);
        Task<CategoryViewModel> UpdateCategoryAsync(int id, UpdateCategoryViewModel model);
        Task<CategoryViewModel> DeleteCategoryAsync(int categoryId);
        Task<CategoryListViewModel> GetPaginatedCategoriesAsync(int pageNumber, int pageSize);
        Task<CategoryViewModel> GetCategoryByIdAsync(int categoryId);
        Task<CategoryViewModel?> GetAsync(Expression<Func<Category, bool>> predicate, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null);

        Task<CategoryListViewModel> GetAllAsync(
            Expression<Func<Category, bool>> predicate,
            Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null
        );
        Task<List<CategoryViewModel>> GetParentCategoriesAsync();
        Task<List<CategoryViewModel>> GetChildCategoriesAsync(int parentId);
    }
}