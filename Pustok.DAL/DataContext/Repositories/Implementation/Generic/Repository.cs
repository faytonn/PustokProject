using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Pustok.DAL.DataContext.Entities.Base;
using Pustok.DAL.DataContext.Paging;
using Pustok.DAL.DataContext.Repositories.Abstraction.Generic;
using System.Linq.Expressions;

namespace Pustok.DAL.DataContext.Repositories.Implementation.Generic;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDBContext _dbContext;
    public Repository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }



    public virtual async Task<T> CreateAsync(T entity)
    {
        var entityEntry = await _dbContext.Set<T>().AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public virtual async Task<T> DeleteAsync(T entity)
    {
        var entityEntry = _dbContext.Set<T>().Remove(entity);

        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (include != null) query = include(query);

        if (orderBy != null) query = orderBy(query);

        query = query.Where(predicate);

        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetAsync(int id)
    {
        var result = await _dbContext.Set<T>().FindAsync(id);

        return result;
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if(include != null)
        {
            query = include(query);
        }

        if(orderBy != null)
        {
            query = orderBy(query);
        }

        query = query.Where(predicate);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Pagination<T>> GetPagesAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true)
    {
       IQueryable<T> queryable = _dbContext.Set<T>();

        if (!enableTracking) queryable = queryable.AsNoTracking();
        
        if(include != null) queryable = include(queryable);

        if(predicate != null) queryable = queryable.Where(predicate);

        if(orderBy != null) queryable = orderBy(queryable);

        return await queryable.PaginateAsync(index, size);
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        var entityEntry = _dbContext.Set<T>().Update(entity);

        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }
}
