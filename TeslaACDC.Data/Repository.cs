using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TeslaACDC.Data.Models;
namespace TeslaACDC.Data;
public class Repository<TId, TEntity> : IRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    internal NikolaContext _context;
    internal DbSet<TEntity> _dbSet;

    public Repository(NikolaContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

        public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(TId id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete is not null)
        {
            await Delete(entityToDelete);
        }
    }

    public virtual async Task<TEntity?> FindAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if(filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(
            new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if(orderBy is not null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    
    }
}

  