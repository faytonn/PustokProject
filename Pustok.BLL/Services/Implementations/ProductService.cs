using AutoMapper;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Pustok.BLL.Helpers.Abstractions;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction.Generic;
using System.Linq.Expressions;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    private readonly ICloudinary _cloudinary;
    private readonly ICloudinaryService _cloudinaryService;

    public ProductService(IRepository<Product> productRepository, IMapper mapper, ICloudinary cloudinary, ICloudinaryService cloudinaryService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _cloudinary = cloudinary;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ProductViewModel> CreateProductAsync(CreateProductViewModel vm)
    {
        var product = _mapper.Map<Product>(vm);
        var createdProduct = await _productRepository.CreateAsync(product);

        return _mapper.Map<ProductViewModel>(createdProduct);
    }

    public async Task<ProductViewModel> UpdateProductAsync(int id, UpdateProductViewModel vm)
    {
        var existProduct = await _productRepository.GetAsync(id);

        if (existProduct == null) { throw new Exception("Product was not found."); }

        existProduct = _mapper.Map(vm, existProduct);

        var updatedProduct = await _productRepository.UpdateAsync(existProduct);

        return _mapper.Map<ProductViewModel>(updatedProduct);
    }

    public async Task<ProductViewModel> DeleteProductAsync(int productId)
    {
        var existProduct = await _productRepository.GetAsync(productId);

        if (existProduct == null) { throw new Exception("Not found"); }

        var deletedProduct = await _productRepository.DeleteAsync(existProduct);

        return _mapper.Map<ProductViewModel>(deletedProduct);
    }

    public async Task<ProductListViewModel> GetPaginatedProductsAsync(int pageNumber, int pageSize)
    {
        var products = await _productRepository.GetPagesAsync(index: pageNumber, size: pageSize);
        var productViewModels = _mapper.Map<List<ProductViewModel>>(products.Items);

        return new ProductListViewModel
        {
            Index = pageNumber,
            Size = pageSize,
            Items = productViewModels,
            Count = products.Count,
            Pages = products.Pages
        };
    }

    public async Task<ProductViewModel> GetProductByIdAsync(int productId)
    {
        var product = await _productRepository.GetAsync(productId);

        if (product == null) { throw new Exception("Not found"); }

        return _mapper.Map<ProductViewModel>(product);
    }


    public async Task<ProductViewModel?> GetAsync(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        var product = await _productRepository.GetAsync(predicate, include);

        if (product == null) { throw new Exception("Not found."); }

        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task<ProductListViewModel> GetAllAsync(Expression<Func<Product, bool>> predicate,
                                Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null,
                                Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null)
    {
        predicate ??= p => true;

        var products = await _productRepository.GetAllAsync(predicate, include, orderBy);

        var productViewModels = _mapper.Map<List<ProductViewModel>>(products);

        var result = new ProductListViewModel
        {
            Items = productViewModels,
            Count = productViewModels.Count,
            Index = 0,
            Size = productViewModels.Count,
            Pages = 1,
            HasPrevious = false,
            HasNext = false
        };

        return result;
    }

    public async Task<ProductListViewModel> GetProductsByCategoryAsync(int categoryId)
    {
        return await GetAllAsync(
        predicate: p => p.CategoryId == categoryId,
        include: q => q.Include(p => p.Category),
        orderBy: q => q.OrderBy(p => p.Name));
    }

    public async Task<ProductListViewModel> GetProductsByTagAsync(int tagId)
    {
        return await GetAllAsync(
        predicate: p => p.ProductTags.Any(pt => pt.TagId == tagId),
        include: q => q.Include(p => p.ProductTags).ThenInclude(pt => pt.Tag),
        orderBy: q => q.OrderBy(p => p.Name));
    }

    public async Task<ProductListViewModel> GetFeaturedProductsAsync()
    {
        return await GetAllAsync(
                predicate: p => p.IsFeatured,
                include: q => q.Include(p => p.Category),
                orderBy: q => q.OrderByDescending(p => p.Rating));
    }

    public async Task<ProductListViewModel> GetDiscountedProductsAsync()
    {
        return await GetAllAsync(
        predicate: p => p.DiscountPrice < p.OriginalPrice,
        include: q => q.Include(p => p.Category),
        orderBy: q => q.OrderBy(p => p.DiscountPrice));
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        var imageUrl = await _cloudinaryService.FileCreateAsync(image);
        return imageUrl;
    }

}
