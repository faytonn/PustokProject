using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.Services.Implementations;
using Pustok.BLL.ViewModels;
using Pustok.BLL.ViewModels.Base;

namespace PustokMiniProject.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Moderator")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;
    private readonly ITagService _tagService;

    public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService, ITagService tagService)
    {
        _productService = productService;
        _mapper = mapper;
        _categoryService = categoryService;
        _tagService = tagService;
    }
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        var categories = await _categoryService.GetPaginatedCategoriesAsync(pageNumber, pageSize);

        return View(categories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        return View(product);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetParentCategoriesAsync();
        var tags = await _tagService.GetTagsForDropdownAsync();

        var vm = new CreateProductViewModel
        {
            Name = "Unnamed Product",
            Description = "Unnamed Description",
            ProductCode = "Unnamed Code",
            Brand = "Unnamed Brand",
            Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList(),
            ProductTags = tags
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetParentCategoriesAsync();
            model.Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            model.ProductTags = await _tagService.GetTagsForDropdownAsync();
            return View(model);
        }

        if (model.ImageUrls != null && model.Images.Any())
        {
            model.ImageUrls = new List<string>();
            foreach (var image in model.Images)
            {
                var imageUrl = await _productService.UploadImageAsync(image);
                model.ImageUrls.Add(imageUrl);
            }
        }

        await _productService.CreateProductAsync(model);
        TempData["SuccessMessage"] = "Product created successfully.";
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Update(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        var categories = await _categoryService.GetParentCategoriesAsync();
        var tags = await _tagService.GetTagsForDropdownAsync();

        var vm = _mapper.Map<UpdateProductViewModel>(product);
        vm.Categories = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
        vm.ProductTags = tags;

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetParentCategoriesAsync();
            model.Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            model.ProductTags = await _tagService.GetTagsForDropdownAsync();
            return View(model);
        }

        if (model.Images != null && model.Images.Any())
        {
            model.ImageUrls = new List<string>();
            foreach (var image in model.Images)
            {
                var imageUrl = await _productService.UploadImageAsync(image);
                model.ImageUrls.Add(imageUrl);
            }
        }

        await _productService.UpdateProductAsync(id, model);
        TempData["SuccessMessage"] = "Product updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteProductAsync(id);
        TempData["SuccessMessage"] = "Product deleted successfully.";
        return RedirectToAction(nameof(Index));
    }


}

