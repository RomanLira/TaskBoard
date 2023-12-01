using System.Linq.Expressions;

namespace Repository.Interfaces;

public interface IRepository<T>
{
    Task<IQueryable<T>> GetAllAsync();  
    Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression);
    Task CreateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveChangesAsync();  
}