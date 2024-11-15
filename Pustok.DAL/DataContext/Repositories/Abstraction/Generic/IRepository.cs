using Microsoft.EntityFrameworkCore.Query;
using Pustok.DAL.DataContext.Entities.Base;
using Pustok.DAL.DataContext.Paging;
using System.Linq.Expressions;

namespace Pustok.DAL.DataContext.Repositories.Abstraction.Generic;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetAsync(int id);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate,
                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate,
                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    Task<Pagination<T>> GetPagesAsync(Expression<Func<T, bool>>? predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                    int index = 0, int size = 10, bool enableTracking = true);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}
