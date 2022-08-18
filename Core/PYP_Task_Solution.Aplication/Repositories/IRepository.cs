using PYP_Task_Solution.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Aplication.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<bool> AddAsync(T model);

    Task<bool> AddRangeAsync(List<T> datas);

    bool Remove(T model);

    Task<bool> RemoveAsync(string id);

    bool RemoveRange(List<T> datas);

    bool Update(T model);

    Task<int> SaveAsync();
    IQueryable<T> GetAll();
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method);
    Task<T> GetByIdAsync(string id);
}