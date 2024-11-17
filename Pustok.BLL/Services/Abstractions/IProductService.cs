using Microsoft.EntityFrameworkCore.Query;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using System.Linq.Expressions;

namespace Pustok.BLL.Services.Abstractions
{
    public interface IProductService
    {
        Task<ProductViewModel> CreateProductAsync(CreateProductViewModel model);
        Task<ProductViewModel> UpdateProductAsync(int id, UpdateProductViewModel model);
        Task<ProductViewModel> DeleteProductAsync(int productId);
        Task<ProductListViewModel> GetPaginatedProductsAsync(int pageNumber, int pageSize);
        Task<ProductViewModel> GetProductByIdAsync(int productId);
        Task<ProductViewModel?> GetAsync(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null);

        Task<ProductListViewModel> GetAllAsync(Expression<Func<Product, bool>> predicate,
                                Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null,
                                Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null);
        Task<ProductListViewModel> GetProductsByCategoryAsync(int categoryId);
        Task<ProductListViewModel> GetProductsByTagAsync(int tagId);
        Task<ProductListViewModel> GetFeaturedProductsAsync();
        Task<ProductListViewModel> GetDiscountedProductsAsync();

        Task<string> UploadImageAsync(IFormFile image);

    }
}
