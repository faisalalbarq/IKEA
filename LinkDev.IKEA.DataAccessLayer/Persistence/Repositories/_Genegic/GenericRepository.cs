using LinkDev.IKEA.DataAccessLayer.Models;
using LinkDev.IKEA.DataAccessLayer.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DataAccessLayer.Persistence.Repositories._Genegic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
            {
                return await _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToListAsync();
            }
            return  await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();
        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Add(T entity) => _dbContext.Set<T>().Add(entity);


        public void Update(T entity) => _dbContext.Set<T>().Update(entity);

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
        }
    }
}
