using LinkDev.IKEA.DataAccessLayer.Persistence.Data;
using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Departments;
using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DataAccessLayer.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository employeeRepository => new EmployeeRepository(_dbContext);
        public IDepartmentRepository departmentRepository => new DepartmentRepository(_dbContext);

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
