using LinkDev.IKEA.DataAccessLayer.Models;
using LinkDev.IKEA.DataAccessLayer.Persistence.Data;
using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories._Genegic;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }


        ///        private readonly ApplicationDbContext _dbContext; 
        ///
        ///        public EmployeeRepository(ApplicationDbContext dbContext)
        ///        {
        ///            _dbContext = dbContext;
        ///        }
        ///
        ///
        ///        public IEnumerable<Employee> GetAll(bool withAsNoTracking = true)
        ///        {
        ///            if (withAsNoTracking)
        ///            {
        ///                return _dbContext.Employees.AsNoTracking().ToList();
        ///            }
        ///            return _dbContext.Employees.ToList();
        ///        }
        ///
        ///        public IQueryable<Employee> GetAllAsIQueryable()
        ///        {
        ///            return _dbContext.Employees;
        ///        }
        ///
        ///        public Employee? Get(int id)
        ///        {
        ///            return _dbContext.Employees.Find(id);
        ///        }
        ///
        ///        public int Add(Employee entity)
        ///        {
        ///            _dbContext.Employees.Add(entity);
        ///            return _dbContext.SaveChanges();
        ///        }
        ///
        ///        public int Update(Employee entity)
        ///        {
        ///            _dbContext.Employees.Update(entity);
        ///            return _dbContext.SaveChanges();
        ///        }
        ///
        ///        public int Delete(Employee entity)
        ///        {
        ///            _dbContext.Employees.Remove(entity);
        ///            return _dbContext.SaveChanges();
        ///        }
        ///
    }
}
