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
        IEnumerable<T> GetAll(bool withAsNoTracking = true);
        IQueryable<T> GetAllAsIQueryable();



        T? Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
