using InvoicingHub.Application.Shared.Contracts.Persistence;
using InvoicingHub.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvoicingHub.Persistence
{
    public class Repository<T, U> : IRepository<T, U>
        where T : class, IGenericEntity<U>
        where U : struct
    {
        private readonly IDatabaseContext _database;

        public Repository(IDatabaseContext databaseContext)
        {
            _database = databaseContext;
        }

        public void Add(T entity)
        {
            _database.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _database.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(ICollection<T> entity)
        {
            await _database.Set<T>().AddRangeAsync(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _database.Set<T>();
        }

        public void Remove(T entity)
        {
            _database.Set<T>().Remove(entity);
        }

        public T Get(U id)
        {
            return _database.Set<T>().FirstOrDefault(p => p.Id.Equals(id));
        }

        public async Task<T> GetAsync(U id)
        {
            return await FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _database.Set<T>().AsQueryable();
            query = query.Where(predicate);
            return await query.FirstOrDefaultAsync();
        }

        public Task<T> UpdateAsync(T entity)
        {
            _database.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }
    }
}