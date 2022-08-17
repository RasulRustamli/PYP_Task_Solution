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
    Task<T> GetSingle(Expression<Func<T, bool>> filter = null);

    Task<bool> AddAsync(T entity);

    Task<bool> UpdateAsync(T entity);

    Task<bool> DeleteAsync(T entity);
    IQueryable<T> GetAll();
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method);
    
    Task<T> GetByIdAsync(string id);
}