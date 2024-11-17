using AutoMapper;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction.Generic;

namespace Pustok.BLL.Services.Implementations;

public class SubscribeService : ISubscribeService
{
    private readonly IRepository<Subscribe> _subscribeRepository;
    private readonly IMapper _mapper;

    public SubscribeService(IRepository<Subscribe> subscribeRepository, IMapper mapper)
    {
        _subscribeRepository = subscribeRepository;
        _mapper = mapper;
    }

    public async Task<SubscribeListViewModel> GetPaginatedSubscribesAsync(int pageNumber, int pageSize)
    {
        var subscribes = await _subscribeRepository.GetPagesAsync(index: pageNumber, size: pageSize);
        var subscribeViewModels = _mapper.Map<List<SubscribeViewModel>>(subscribes.Items);

        return new SubscribeListViewModel
        {
            Items = subscribeViewModels,
            Count = subscribes.Count,
            Index = pageNumber,
            Size = pageSize,
            Pages = subscribes.Pages
        };
    }

    public async Task<SubscribeViewModel> GetSubscribeByIdAsync(int id)
    {
        var subscribe = await _subscribeRepository.GetAsync(id);
        if (subscribe == null) { throw new Exception("Subscribe not found."); }

        return _mapper.Map<SubscribeViewModel>(subscribe);
    }

    public async Task<SubscribeViewModel> CreateSubscribeAsync(CreateSubscribeViewModel model)
    {
        var subscribe = _mapper.Map<Subscribe>(model);
        var createdSubscribe = await _subscribeRepository.CreateAsync(subscribe);

        return _mapper.Map<SubscribeViewModel>(createdSubscribe);
    }

    public async Task<SubscribeViewModel> UpdateSubscribeAsync(int id, UpdateSubscribeViewModel model)
    {
        var existingSubscribe = await _subscribeRepository.GetAsync(id);
        if (existingSubscribe == null) { throw new Exception("Subscribe not found."); }

        _mapper.Map(model, existingSubscribe);
        var updatedSubscribe = await _subscribeRepository.UpdateAsync(existingSubscribe);

        return _mapper.Map<SubscribeViewModel>(updatedSubscribe);
    }

    public async Task<SubscribeViewModel> DeleteSubscribeAsync(int id)
    {
        var subscribe = await _subscribeRepository.GetAsync(id);
        if (subscribe == null) { throw new Exception("Subscribe not found."); }

        var deletedSubscribe = await _subscribeRepository.DeleteAsync(subscribe);
        return _mapper.Map<SubscribeViewModel>(deletedSubscribe);
    }
}