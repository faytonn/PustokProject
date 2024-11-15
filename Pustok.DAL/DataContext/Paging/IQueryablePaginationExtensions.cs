using Microsoft.EntityFrameworkCore;

namespace Pustok.DAL.DataContext.Paging;

public static class IQueryablePaginationExtensions
{
    public static async Task<Pagination<T>> PaginateAsync<T>(this IQueryable<T> source,int index, int size)
    {
        int count = await source.CountAsync();
        List<T> items = await source.Skip(index * size).Take(size).ToListAsync();

        Pagination<T> list = new()
        {
            Index = index,
            Size = size,
            Items = items,
            Count = count,
            Pages = (int)Math.Ceiling(count / (double)size)
        };

        return list;
    }
}
