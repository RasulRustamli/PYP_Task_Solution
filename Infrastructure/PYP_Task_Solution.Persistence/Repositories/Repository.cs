using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PYP_Task_Solution.Aplication.Repositories;
using PYP_Task_Solution.Domain.Entities.Common;
using PYP_Task_Solution.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly PYP_Task_SolutionDbContext _context;
    public Repository(PYP_Task_SolutionDbContext context)
    {
        _context = context;
    }
    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> datas)
    {

        await Table.AddRangeAsync(datas);
        return true;
    }

    public IQueryable<T> GetAll()
    {
        try
        {
            var query = Table.AsQueryable();
            return query;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public  async Task<T> GetByIdAsync(string id)
    {
        var query = Table.AsQueryable();
        return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
    {
        var query = Table.AsQueryable();
        return await query.FirstOrDefaultAsync(method);
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
    {
        var query = Table.Where(method);
        return query;
    }

    public bool Remove(T model)
    {
        EntityEntry<T> entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    public  async Task<bool> RemoveAsync(string id)
    {
        T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        return Remove(model);
    }

    public bool RemoveRange(List<T> datas)
    {
        Table.RemoveRange(datas);
        return true;
    }

        
    public  async Task<int> SaveAsync() => await _context.SaveChangesAsync();
   

    public bool Update(T model)
    {
        EntityEntry entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }
}

