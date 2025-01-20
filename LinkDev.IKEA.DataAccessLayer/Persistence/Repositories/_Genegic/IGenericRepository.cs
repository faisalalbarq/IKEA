using LinkDev.IKEA.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DataAccessLayer.Persistence.Repositories._Genegic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true);
        Task<T?> GetAsync(int id);

        IQueryable<T> GetAllAsIQueryable();


        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
