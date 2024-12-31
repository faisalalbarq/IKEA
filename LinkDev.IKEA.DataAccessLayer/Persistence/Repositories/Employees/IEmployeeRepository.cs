using LinkDev.IKEA.DataAccessLayer.Models.Employees;
using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories._Genegic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Employees
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
       
    }
}
