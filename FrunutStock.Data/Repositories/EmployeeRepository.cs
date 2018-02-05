using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Model.Models;

namespace FrunutStock.Data.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public Employee GetEmployeeById(Int64 employeeId)
        {
            var employee = this.DbContext.Employees.Where(e => e.ID == employeeId).FirstOrDefault();

            return employee;
        }

        public Employee GetEmployeeByName(string employeeName)
        {
            var employee = this.DbContext.Employees.Where(e => e.FullName == employeeName).FirstOrDefault();

            return employee;
        }

        
    }

    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee GetEmployeeById(Int64 employeeId);
        Employee GetEmployeeByName(string employeeName);
    }
}
