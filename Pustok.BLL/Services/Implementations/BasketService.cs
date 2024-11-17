using AutoMapper;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction.Generic;
using System.Reflection.Metadata;

namespace Pustok.BLL.Services.Implementations;

public class BasketService : IBasketService
{
    private readonly IRepository<BasketItem> _basketRepository;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly string BASKET_KEY = "basket";

    public BasketService(IRepository<BasketItem> basketRepository, IMapper mapper, IProductService productService, IHttpContextAccessor contextAccessor)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
        _productService = productService;
        _contextAccessor = contextAccessor;
    }

    public async Task<BasketItemViewModel> AddToBasketAsync(int productId, bool useDatabase)
    {
        if(useDatabase)
        {
            var basketItem = await _basketRepository.GetAsync(x => x.ProductId == productId);

            if(basketItem == null)
            {
                basketItem = new BasketItem
                {
                    ProductId = productId,
                    Quantity = 1,
                    UserId = _contextAccessor.HttpContext?.User?.Identity?.Name
                };
                await _basketRepository.CreateAsync(basketItem);
            }
            else
            {
                basketItem.Quantity++;
                await _basketRepository.UpdateAsync(basketItem);
            }

            return _mapper.Map<BasketItemViewModel>(basketItem);
        }
        else
        {
            var product = await _productService.GetAsync(p => p.Id == productId);
            if (product == null) throw new Exception("Product not found.");

            var basketItems = GetCookieBasketItems();
            var existingItem = basketItems.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var basketItem = new BasketItemViewModel
                {
                    ProductId = productId,
                    Quantity = 1,
                    ProductName = product.Name,
                    Price = product.DiscountPrice > 0 ? product.DiscountPrice : product.OriginalPrice,
                    ImageUrl = product.ImageUrls.FirstOrDefault()
                };
                basketItems.Add(basketItem);
            }

            SetCookieBasketItems(basketItems);
            return existingItem ?? basketItems.Last();
        }
    }

    public async Task<BasketItemViewModel> IncreaseBasketItemAsync(int productId, bool useDatabase)
    {
        if (useDatabase)
        {
            var basketItem = await _basketRepository.GetAsync(x => x.ProductId == productId);
            if (basketItem == null) { throw new Exception("Basket item not found."); }

            basketItem.Quantity++;
            await _basketRepository.UpdateAsync(basketItem);

            return _mapper.Map<BasketItemViewModel>(basketItem);
        }
        else
        {
            var basketItems = GetCookieBasketItems();
            var basketItem = basketItems.FirstOrDefault(x => x.ProductId == productId);

            if (basketItem == null) throw new Exception("Basket item not found.");

            basketItem.Quantity++;
            SetCookieBasketItems(basketItems);

            return basketItem;
        }
    }
    public async Task<BasketItemViewModel> DecreaseBasketItemAsync(int productId, bool useDatabase)
    {
        if (useDatabase)
        {
            var basketItem = await _basketRepository.GetAsync(x => x.ProductId == productId);
            if (basketItem == null) throw new Exception("Basket item not found.");

            if (basketItem.Quantity > 1)
            {
                basketItem.Quantity--;
                await _basketRepository.UpdateAsync(basketItem);
            }
            else
            {
                await _basketRepository.DeleteAsync(basketItem);
            }

            return _mapper.Map<BasketItemViewModel>(basketItem);
        }
        else
        {
            var basketItems = GetCookieBasketItems();
            var basketItem = basketItems.FirstOrDefault(x => x.ProductId == productId);

            if (basketItem == null) throw new Exception("Basket item not found.");

            if (basketItem.Quantity > 1)
            {
                basketItem.Quantity--;
            }
            else
            {
                basketItems.Remove(basketItem);
            }

            SetCookieBasketItems(basketItems);

            return basketItem;
        }
    }

    public async Task RemoveFromBasketAsync(int productId, bool useDatabase)
    {
        if (useDatabase)
        {
            var basketItem = await _basketRepository.GetAsync(x => x.ProductId == productId);
            if (basketItem == null) throw new Exception("Basket item not found.");

            await _basketRepository.DeleteAsync(basketItem);
        }
        else
        {
            var basketItems = GetCookieBasketItems();
            basketItems.RemoveAll(x => x.ProductId == productId);

            SetCookieBasketItems(basketItems);
        }
    }
    public async Task<List<BasketItemViewModel>> GetBasketItemsAsync(bool useDatabase)
    {
        if (useDatabase)
        {
            var basketItems = await _basketRepository.GetAllAsync(x => true);
            return _mapper.Map<List<BasketItemViewModel>>(basketItems);
        }
        else
        {
            return GetCookieBasketItems();
        }
    }

    private List<BasketItemViewModel> GetCookieBasketItems()
    {
        var basketJson = _contextAccessor.HttpContext?.Request.Cookies[BASKET_KEY];
        return basketJson != null
            ? JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketJson) ?? new List<BasketItemViewModel>()
            : new List<BasketItemViewModel>();
    }

    private void SetCookieBasketItems(List<BasketItemViewModel> basketItems)
    {
        var basketJson = JsonConvert.SerializeObject(basketItems);
        _contextAccessor.HttpContext?.Response.Cookies.Append(BASKET_KEY, basketJson, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });
    }

}
