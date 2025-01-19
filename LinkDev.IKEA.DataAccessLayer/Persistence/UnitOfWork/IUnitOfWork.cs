using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Departments;
using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DataAccessLayer.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository employeeRepository { get;}
        public IDepartmentRepository departmentRepository { get;}

        int complete();
    }
}
