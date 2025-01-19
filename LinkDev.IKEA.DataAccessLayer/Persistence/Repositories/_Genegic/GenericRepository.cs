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


        public IEnumerable<T> GetAll(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
            {
                return _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToList();
            }
            return _dbContext.Set<T>().Where(X => !X.IsDeleted).ToList();
        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public T? Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
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
