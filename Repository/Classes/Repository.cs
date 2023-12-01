using System.Linq.Expressions;
using Repository.Interfaces;

namespace Repository.Classes;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationContext _applicationContext;

    public Repository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression) =>
        await Task.Run(() => _applicationContext.Set<T>().Where(expression));

    public async Task<IQueryable<T>> GetAllAsync() =>
        await Task.Run(() => _applicationContext.Set<T>());

    public async Task CreateAsync(T entity) =>
        await Task.Run(() => _applicationContext.Set<T>().Add(entity));

    public async Task DeleteAsync(T entity) =>
        await Task.Run(() => _applicationContext.Set<T>().Remove(entity));

    public async Task SaveChangesAsync() =>
        await Task.Run(() => _applicationContext.SaveChangesAsync());
}